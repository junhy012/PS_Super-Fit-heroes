using UnityEngine;

public class Questions : MonoBehaviour
{
    public GameObject questionUI;
    public Transform nextMapSpawn;

    private void ShowQuestion()
    {
        if (questionUI != null)
        {
            questionUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void AnswerQuestion(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Correct answer! Teleporting player...");

            if (questionUI != null)
                questionUI.SetActive(false);

            Time.timeScale = 1f;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && nextMapSpawn != null)
            {
                player.transform.position = nextMapSpawn.position;
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

    private void OnTriggerEnter2D(Collider2D other)
        {
    if (other.CompareTag("Player"))
    {
        ShowQuestion();
    }
    }


    public void CorrectAnswer()
    {
        AnswerQuestion(true);
    }

    public void WrongAnswer()
    {
        AnswerQuestion(false);
    }
}
