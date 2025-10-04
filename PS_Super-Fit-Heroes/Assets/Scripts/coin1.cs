using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Text Object Prefab")]
    public GameObject textObjectPrefab; // Assign your UI prefab here

    [Header("Text Duration")]
    public float displayTime = 2f; // Seconds to keep the message visible

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player") && textObjectPrefab != null)
        {
            hasTriggered = true;

            // Find the first Canvas in the scene
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                // Instantiate the text object under the canvas
                GameObject textInstance = Instantiate(textObjectPrefab, canvas.transform);

                // Optional: center the UI text
                RectTransform rt = textInstance.GetComponent<RectTransform>();
                if (rt != null)
                    rt.anchoredPosition = Vector2.zero;

                // OPTIONAL: Remove text after displayTime
                // If you want it to stay forever, remove this line
                StartCoroutine(RemoveTextAfterDelay(textInstance, displayTime));
            }

            // The coin is NOT destroyed or picked up
        }
    }

    private IEnumerator RemoveTextAfterDelay(GameObject textObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(textObject); // Remove this line if you want the text to stay forever
    }
}
