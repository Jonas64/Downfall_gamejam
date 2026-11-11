using UnityEngine;
using UnityEngine.Video;

public class videoplayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer targetVideoPlayer;
    private bool hasPlayed = false;

    private void Start()
    {
        if (targetVideoPlayer != null)
        {
            // Tell Unity to run the "OnVideoFinished" function when the video ends
            targetVideoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void OnDestroy()
    {
        if (targetVideoPlayer != null)
        {
            // Clean up the event listener if this object is destroyed
            targetVideoPlayer.loopPointReached -= OnVideoFinished;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (targetVideoPlayer != null)
            {
                // Make sure the video object is active before playing
                targetVideoPlayer.gameObject.SetActive(true);
                targetVideoPlayer.Play();
                hasPlayed = true;
            }
        }
    }

    // This function automatically runs the exact frame the video ends
    private void OnVideoFinished(VideoPlayer source)
    {
        // Hide the video object entirely
        source.gameObject.SetActive(false);
    }
}