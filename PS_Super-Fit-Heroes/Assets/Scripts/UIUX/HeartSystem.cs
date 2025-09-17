using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // for scene loading

public class HeartSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public float health;
    public int maxHealth;

    [Header("Heart Sprites")]
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    [Header("Player Reference")]
    public PlayerController playerHealth;

    [Header("Game Over Settings")]
    [Tooltip("The name of your Start Scene (must match Build Settings).")]
    public string startSceneName = "StartScene";

    void Update()
    {
        // Sync health with player
        health = playerHealth.currentHp;
        maxHealth = playerHealth.maxHp;

        // Update heart UI
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Enable only up to max health
            hearts[i].enabled = (i < maxHealth);
        }

        // Check for game over
        if (health <= 0)
        {
            OnPlayerDeath();
        }
    }

    /// <summary>
    /// Handles the transition when health is 0.
    /// </summary>
    void OnPlayerDeath()
    {
        Debug.Log("Player health is 0. Loading start scene...");

        // Prevent looping if Update keeps firing
        enabled = false;

        // Safety check: only load if scene is in build settings
        if (Application.CanStreamedLevelBeLoaded(startSceneName))
        {
            SceneManager.LoadScene(startSceneName);
        }
        else
        {
            Debug.LogError($"Start scene '{startSceneName}' not found in Build Settings!");
        }
    }
}
