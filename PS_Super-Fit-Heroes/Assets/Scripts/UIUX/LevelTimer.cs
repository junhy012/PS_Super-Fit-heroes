using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float totalTime = 360f;

    private float currentTime;
    
    public TextMeshProUGUI timerText;

    private bool isRunning = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = totalTime;
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
        Debug.Log("done");
    }
}
