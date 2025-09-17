using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Text Object Prefab")]
    public GameObject textObjectPrefab; // assign Text1 prefab here

    [Header("Text Duration")]
    public float displayTime = 2f; // seconds before the text disappears

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textObjectPrefab != null)
        {
            // Find the Canvas in the scene
            Canvas canvas = Canvas.FindFirstObjectByType<Canvas>();
            if (canvas != null)
            {
                // Instantiate the prefab as a child of the Canvas
                GameObject textInstance = Instantiate(textObjectPrefab, canvas.transform);

                // Optional: center it on the screen
                RectTransform rt = textInstance.GetComponent<RectTransform>();
                if (rt != null)
                    rt.anchoredPosition = Vector2.zero;

                // Destroy after displayTime seconds
                Destroy(textInstance, displayTime);
            }

            // Destroy the coin
            Destroy(gameObject);
        }
    }
}
