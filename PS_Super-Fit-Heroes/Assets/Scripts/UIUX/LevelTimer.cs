using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float totalTime;

    private float currentTime;
    
    public TextMeshProUGUI timerText;
    
    public Transform spawnPoint;
    
    GameObject player;

    private bool isRunning = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = totalTime;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;
        
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;
            OnTimerEnd();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void OnTimerEnd()
    {
        currentTime = totalTime;
        player.transform.position = spawnPoint.position;
    }
}
