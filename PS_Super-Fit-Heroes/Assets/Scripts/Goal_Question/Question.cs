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

    public void AnswerQuestion(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Correct answer! Teleporting player...");

            if (questionUI != null)
                questionUI.SetActive(false);

            Time.timeScale = 1f;

            // Develop by minhh but commenting it out so i can show completionscene
            //SceneManager.LoadScene(NextScene);

            //By Ashish, adding this small chunk of code to load the completion scene
            PlayerPrefs.SetString("lc_nextSceneName", NextScene);
            PlayerPrefs.Save();
            SceneManager.LoadScene("CompletionScene");
                
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
        Debug.Log("Correct!");
        AnswerQuestion(true);
    }

    public void WrongAnswer()
    {
        AnswerQuestion(false);
    }
}