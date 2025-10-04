using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private float delaySeconds = 2f;
    [SerializeField] private string completionSceneName = "CompletionScene";

    private const string NextIndexKey = "nextBuildIndex";
    private const string NextNameKey = "lc_nextSceneName";


    public void OnQuizPassed()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        PlayerPrefs.SetInt(NextIndexKey, nextIndex);
        PlayerPrefs.DeleteKey(NextNameKey);
        PlayerPrefs.Save();

        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene(completionSceneName);
    }

    private void Start()
    {
        // Only auto-advance when we're actually on the completion scene
        if (SceneManager.GetActiveScene().name != completionSceneName) return;


        // string nextByName = PlayerPrefs.GetString(NextNameKey, "");
        // if (!string.IsNullOrEmpty(nextByName))
        // {
        //     Invoke(nameof(LoadByName), delaySeconds);
        //     return;
        // }

        // // Fallback: index-based next scene
        // int nextIndex = PlayerPrefs.GetInt(NextIndexKey, -1);
        // if (nextIndex >= 0 && nextIndex < SceneManager.sceneCountInBuildSettings)
        // {
        //     Invoke(nameof(LoadByIndex), delaySeconds);
        // }
        // else
        // {
        //     Debug.Log("No next scene set. Staying on CompletionScene.");
        // }

        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
    }

    public void LoadNextLevelByButton()
    {
        //unpausing the game time
        Time.timeScale = 1f;

        //checking for scene name first
        string nextScene = PlayerPrefs.GetString(NextNameKey, "");
        if (!string.IsNullOrEmpty(nextScene))
        {
            LoadByName();
            return;
        }

        int nextIndex = PlayerPrefs.GetInt(NextIndexKey, -1);
        if (nextIndex >= 0 && nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadByIndex();
            return;
        }

        Debug.LogError("ERROR: The Next Level button was clicked, but no valid next scene name or index was found in PlayerPrefs.");
    }

    private void LoadByName()
    {
        string next = PlayerPrefs.GetString(NextNameKey, "");
        if (!string.IsNullOrEmpty(next))
            SceneManager.LoadScene(next);
    }

    private void LoadByIndex()
    {
        int nextIndex = PlayerPrefs.GetInt(NextIndexKey, -1);
        if (nextIndex >= 0 && nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
    }
}
