using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPopup : MonoBehaviour
{
    public string information;

    public TextMeshProUGUI informationText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        informationText.text = information;
    }

    private void OnEnable()
    {
        Invoke("HidePopup",3);
    }
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
