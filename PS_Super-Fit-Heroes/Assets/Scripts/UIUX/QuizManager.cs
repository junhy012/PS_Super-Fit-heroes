using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public string question;
    public string[] answers;
    
    public TextMeshProUGUI questionTxt;
    public TextMeshProUGUI button1;
    public TextMeshProUGUI button2;
    public TextMeshProUGUI button3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button1.text = answers[0];
        button2.text = answers[1];
        button3.text = answers[2];
        setQuiz();
    }

    void setQuiz()
    {
        questionTxt.text = question;
    }

    void wrongAnswer()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
