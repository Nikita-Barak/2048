using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // Get the main camera reference
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check if the figure is out of bounds
        if (IsOutOfCameraView())
        {
            // Notify LevelControl about the Game Over
            LevelControl.instance.GameOver();

            // Destroy the figure
            Destroy(gameObject);
        }
    }

    private bool IsOutOfCameraView()
    {
        // Convert the figure's position to viewport space
        var viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the figure is outside the camera's view
        return viewportPosition.y < 0 || viewportPosition.x < 0 || viewportPosition.x > 1;
    }
}