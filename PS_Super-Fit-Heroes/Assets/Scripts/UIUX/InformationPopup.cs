using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPopup : MonoBehaviour
{
    public string information;

    public TextMeshProUGUI informationText;

    public float typingSpeed = 0.05f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // informationText.text = information;
    }

    private void OnEnable()
    {
        informationText.text = "";
        StartCoroutine(TypeText());
        Invoke("HidePopup",3);
    }

    private IEnumerator TypeText()
    {
        foreach (char c in information.ToCharArray())
        {
            informationText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
