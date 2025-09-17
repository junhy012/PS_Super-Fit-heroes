using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CompletionScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //this will load the next scene in build index
        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
