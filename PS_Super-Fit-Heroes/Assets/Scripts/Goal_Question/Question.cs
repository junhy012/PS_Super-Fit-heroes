using UnityEngine;
using UnityEngine.SceneManagement;

public class Questions : MonoBehaviour
{
    public GameObject questionUI;
    // public Transform nextMapSpawn;

    [SerializeField]
    public string NextScene;
    private void ShowQuestion()
    {
        if (questionUI != null)
        {
            questionUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowQuestion();
        }
    }


    public void CorrectAnswer()
    {
        Debug.Log("Correct!");
        AnswerQuestion(true);
    }

    public void WrongAnswer()
    {
        AnswerQuestion(false);
    }
    
    public Transform respawnPoint;  

    public void AnswerQuestion(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Correct answer! Checking items...");
            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

            if (inventory != null && inventory.HasAllItems())
            {
                Debug.Log("All items collected! Proceeding to next scene...");
                if (questionUI != null)
                    questionUI.SetActive(false);

                Time.timeScale = 1f;
                SceneManager.LoadScene(NextScene);
            }
            else
            {
                Debug.Log("Not enough items! Restarting level...");
                if (questionUI != null)
                    questionUI.SetActive(false);

                Time.timeScale = 1f;

             
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null && respawnPoint != null)
                {
                    player.transform.position = respawnPoint.position;
                }

       
                if (inventory != null)
                    inventory.ResetItems();
            }
        }
        else
        {
            Debug.Log("Wrong answer! Try again.");

            if (questionUI != null)
                questionUI.SetActive(false);

            Time.timeScale = 1f;
        }
    }
}