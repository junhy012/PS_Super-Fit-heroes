using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject hintUI; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hintUI != null)
                hintUI.SetActive(true);  
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hintUI != null)
                hintUI.SetActive(false); 
        }
    }
}
