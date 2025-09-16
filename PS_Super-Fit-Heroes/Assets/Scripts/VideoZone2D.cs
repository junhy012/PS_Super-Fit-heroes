using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class VideoZone2D : MonoBehaviour
{
    [Tooltip("Drag the VideoScreen (with MP4VideoController) here")]
    public MP4VideoController videoController;

    [Tooltip("Optional: renderer of the VideoScreen to show/hide (e.g., MeshRenderer on the Quad)")]
    public Renderer screenRenderer;

    [Tooltip("If true, pause on exit; if false, stop on exit")]
    public bool pauseOnExit = true;

    void Reset()
    {
        // Ensure this collider is set as a trigger
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    void Awake()
    {
        if (screenRenderer == null && videoController != null)
        {
            // Try to auto-grab a Renderer from the same GameObject as the controller
            screenRenderer = videoController.GetComponent<Renderer>();
        }
        SetScreenVisible(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || videoController == null) return;
        SetScreenVisible(true);
        videoController.Play();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || videoController == null) return;
        if (!videoController.IsPlaying)
        {
            // If something paused/stopped it, keep it playing while we’re inside
            videoController.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || videoController == null) return;
        //if (pauseOnExit) videoController.Pause();
        //else videoController.Stop();
        videoController.Stop();
        SetScreenVisible(false);
    }

    private void SetScreenVisible(bool visible)
    {
        if (screenRenderer != null) screenRenderer.enabled = visible;
    }
}
