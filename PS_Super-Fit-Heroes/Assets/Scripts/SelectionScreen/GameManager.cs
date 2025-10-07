using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerCharacter;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

   public void SetPlayerCharacter(GameObject playerCharacter)
    {
        this.playerCharacter = playerCharacter;
    }
}
