using System.Collections;
using UnityEngine;

public class Cap : MonoBehaviour
{
    public static Cap instance;

    [SerializeField] private float timeToClose = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Enable()
    {
        StartCoroutine("ContainExplosion");
    }

    private IEnumerator ContainExplosion()
    {
        var capCollider = instance.GetComponent<BoxCollider2D>();
        capCollider.enabled = true; // Enable jar barrier
        Debug.Log("Cap is closed");
        yield return new WaitForSeconds(timeToClose); // Duration of the containment
        capCollider.enabled = false; // Disable jar barrier
        Debug.Log("Cap is open");
    }
}