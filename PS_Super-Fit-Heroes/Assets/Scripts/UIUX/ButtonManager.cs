using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button button;

    public bool isCorrect;
    
    public Color defaultColor = Color.white;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        defaultColor = button.image.color;
        
        button.onClick.AddListener(OnClickButton);
    }
    
    public void OnClickButton()
    {
        if (isCorrect)
        {
            Debug.Log("Correct!");
            button.image.color = Color.green;
        }
        else
        {
            Debug.Log("Incorrect!");
            button.image.color = Color.red;
        }
    }

    public void ResetColor()
    {
        button.image.color = defaultColor;
    }
    

    // Update is called once per frame
    void Update()
    {
       
    }
}
