using Unity.Mathematics.Geometry;
using UnityEngine;

public class RandomFigureSpawner : MonoBehaviour
{
    public static RandomFigureSpawner Instance { get; private set; } // Singleton instance

    public GameObject figurePrefab;   // Prefab for figures
    public Sprite[] figureSprites;   // Array of default sprites for random figures
    
    private GameObject currentFigure;

    [Header("Number settings")] 
    public int maxNumberPow;
    
    public int currentNumberPow {get; private set;}

    private void Awake()
    {
        // Ensure there is only one instance of the spawner
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate spawners
        }
    }

    private void Start()
    {
        FillFigures();
        SpawnRandomFigure();
    }

    public void SpawnRandomFigure()
    {
        if (figurePrefab == null || figureSprites == null || figureSprites.Length == 0) return;

        // Create a new GameObject from the prefab
        currentFigure = Instantiate(figurePrefab);

        // Set a random sprite for the figure
        SetRandomSprite(currentFigure);

        // Set its position randomly within the spawn area
        Vector2 spawnPosition = new Vector2(
            Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2),
            transform.position.y
        );
        currentFigure.transform.position = spawnPosition;
    }

    private void SetRandomSprite(GameObject figure)
    {
        SpriteRenderer spriteRenderer = figure.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("Figure prefab is missing a SpriteRenderer component!");
            return;
        }

        // Assign a random sprite from the array
        Sprite randomSprite = figureSprites[Random.Range(0, figureSprites.Length)];
        spriteRenderer.sprite = randomSprite;

        // Update the PolygonCollider2D to match the new sprite
        PolygonCollider2D polygonCollider = figure.GetComponent<PolygonCollider2D>();
        if (polygonCollider != null)
        {
            Destroy(polygonCollider); // Remove the old collider
            polygonCollider = figure.AddComponent<PolygonCollider2D>(); // Recreate it to auto-adjust to the new sprite
        }
        
        Rigidbody2D rb = figure.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void FillFigures()
    {
        // Load default sprites dynamically
        figureSprites = Resources.LoadAll<Sprite>("Figures"); // Folder inside Resources/Figures
    }

    public void increasePow()
    {
        if (currentNumberPow < maxNumberPow && currentNumberPow < System.Math.Sqrt(Score.instance.maxScore))
        {
            currentNumberPow++;
        }
    }
}