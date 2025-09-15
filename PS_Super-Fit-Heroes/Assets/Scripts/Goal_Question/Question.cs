using UnityEngine;

public class GoalArea : MonoBehaviour
{
    public GameObject questionUI; // UI panel for the question

    private bool playerReached = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerReached && other.CompareTag("Player"))
        {
            playerReached = true;
            ShowQuestion();
        }
    }

    private void ShowQuestion()
    {
        if (questionUI != null)
        {
            questionUI.SetActive(true); // Show the question UI
            Time.timeScale = 0f; // Optional: pause the game while answering
        }
    }

    public void AnswerQuestion(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Correct answer! Proceed.");
        }
        else
        {
            Debug.Log("Wrong answer! Try again.");
        }

        if (questionUI != null)
        {
            questionUI.SetActive(false);
            Time.timeScale = 1f; // Resume the game
        }
    }
}
