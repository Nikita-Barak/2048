using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class Figure : MonoBehaviour
{
    [Header("Move settings")]
    [SerializeField] private float moveSpeed = 10;

    [SerializeField] private float rotationSpeed = 100f;

    [Header("Scale Settings")]
    [SerializeField] private float growSpeed = .1f;

    [SerializeField] private float maxScale = 3f;
    [SerializeField] private float massSpeedScale = 0.5f;

    [Header("Explotion Settings")]
    [SerializeField] private float explosionRadius = 1f; // Radius of the explosion

    [SerializeField] private float explosionForce = 200f; // Max force applied to figures
    private Collider2D col;
    private bool isHolding = true;

    private Rigidbody2D rb;
    private TextMeshPro tm;
    
    private float xInput;

    public int number { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        tm = GetComponentInChildren<TextMeshPro>();

        number = generateNum();
        UpdateFigure();
    }

    private void Update()
    {
        if (isHolding)
        {
            Move();
            Rotate();
            Release();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the Figure script
        var otherFigure = collision.gameObject.GetComponent<Figure>();

        if (otherFigure != null && otherFigure.number == number)
        {
            // Double this figure's number
            number *= 2;
            RandomFigureSpawner.Instance.increasePow();
            Score.instance.SetScore(number);

            UpdateFigure();


            Destroy(otherFigure.gameObject);
            Cap.instance.Enable();
            TriggerExplosionPhysics();
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void Move()
    {
        // Get player input
        xInput = Input.GetAxis("Horizontal");

        // Calculate new position based on input
        var newPosition = rb.position + new Vector2(xInput * moveSpeed * Time.deltaTime, 0);

        // Clamp the new X position within the borders
        newPosition.x = Mathf.Clamp(newPosition.x, LevelControl.instance.leftBorder, LevelControl.instance.rightBorder);

        // Move the Rigidbody2D to the clamped position
        rb.MovePosition(newPosition);
    }

    private void Rotate()
    {
        // Check for rotation input
        if (Input.GetKey(KeyCode.Q))
        {
            // Rotate counter-clockwise
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            // Rotate clockwise
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }

    private void Release()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            isHolding = false;
            StartCoroutine("WaitAndRespawn");
        }
    }

    private void UpdateFigure()
    {
        SetText();
        AdjustScale();
        UpdateColor();
        AdjustMass();
    }

    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(1);
        RandomFigureSpawner.Instance.SpawnRandomFigure();
    }

    private int generateNum()
    {
        // Weighted probabilities for lower powers
        var weights = new float[RandomFigureSpawner.Instance.currentNumberPow];
        float totalWeight = 0;

        // Assign weights (higher for lower powers)
        for (var i = 0; i < weights.Length; i++)
        {
            weights[i] = 1f / (i + 1); // Example: Inverse of the power index
            totalWeight += weights[i];
        }

        // Generate a random value within the total weight
        var randomValue = Random.Range(0, totalWeight);

        // Determine which power corresponds to the random value
        float cumulativeWeight = 0;
        var chosenPow = 1;
        for (var i = 0; i < weights.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight)
            {
                chosenPow = i + 1; // Powers start from 1
                break;
            }
        }

        // Return the number corresponding to the chosen power
        return (int)Math.Pow(2, chosenPow);
    }

    private void SetText()
    {
        tm.text = number.ToString();
    }

    private void AdjustMass()
    {
        if (rb != null)
        {
            // Set mass based on the number, scale smoothly with logarithm
            rb.mass = Mathf.Log(number, 4) * massSpeedScale;
        }
    }

    private void AdjustScale()
    {
        // Base scale (your prefab's default)
        var baseScale = transform.localScale.x;

        // Calculate the scale multiplier based on the number's logarithm
        var scaleMultiplier = Mathf.Log(number, 4) * growSpeed;

        // Apply the new scale
        var newScale = baseScale + scaleMultiplier;

        newScale = Mathf.Min(newScale, maxScale);

        transform.localScale = new Vector3(newScale, newScale, 1); // Keep Z as 1 for 2D
    }

    private Color GetColorForNumber(int number)
    {
        switch (number)
        {
            case 2: return new Color(0.93f, 0.89f, 0.85f); // Light Gray
            case 4: return new Color(0.93f, 0.88f, 0.75f); // Pale Yellow
            case 8: return new Color(0.95f, 0.69f, 0.47f); // Light Orange
            case 16: return new Color(0.96f, 0.58f, 0.39f); // Orange
            case 32: return new Color(0.96f, 0.48f, 0.37f); // Red-Orange
            case 64: return new Color(0.96f, 0.38f, 0.23f); // Bright Red
            case 128: return new Color(0.93f, 0.81f, 0.45f); // Gold
            case 256: return new Color(0.93f, 0.79f, 0.34f); // Yellow-Gold
            case 512: return new Color(0.93f, 0.77f, 0.22f); // Bright Yellow
            case 1024: return new Color(0.93f, 0.75f, 0.11f); // Pale Yellow
            case 2048: return new Color(0.93f, 0.73f, 0.0f); // Vibrant Yellow
            default: return Color.white; // Default for higher numbers
        }
    }

    private void UpdateColor()
    {
        // Get the SpriteRenderer component
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Assign the color based on the number
            spriteRenderer.color = GetColorForNumber(number);
        }

        if (number > 4)
        {
            tm.color = Color.white;
        }
    }

    public void TriggerExplosionPhysics()
    {
        var nearbyFigures = Physics2D.OverlapCircleAll(transform.position, explosionRadius);


        foreach (var col in nearbyFigures)
        {
            var rb = col.GetComponent<Rigidbody2D>();

            if (rb != null && rb != this.rb)
            {
                var forceDirection = (rb.position - (Vector2)transform.position).normalized;
                var forceMagnitude = explosionForce / rb.mass;

                // Apply force
                rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
            }
        }
    }
}