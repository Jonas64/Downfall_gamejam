using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    [Tooltip("Drag your UI Canvas GameObject here")]
    public GameObject uiCanvas;

    private void Start()
    {
        // Hide the UI Canvas when the game starts
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Show the Canvas when the Player enters the collider
        if (other.CompareTag("Player") && uiCanvas != null)
        {
            uiCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Hide the Canvas when the Player leaves the collider
        if (other.CompareTag("Player") && uiCanvas != null)
        {
            uiCanvas.SetActive(false);
        }
    }
}