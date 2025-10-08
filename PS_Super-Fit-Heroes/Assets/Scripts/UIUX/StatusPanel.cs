using System;
using TMPro;
using UnityEngine;

public class StatusPanel : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    public Transform[] statusObj = new Transform[4];

    public TextMeshProUGUI[] levelText = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] valueText = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] needText = new TextMeshProUGUI[4];

    CanvasGroup group;
    
    Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
        RefreshUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            animator.SetBool("isEnable",true);
            RefreshUI();
        }
        
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            animator.SetBool("isEnable",false);
        }
    }

    public void RefreshUI()
    {
        SetLevel();
        SetValue();
        SetNeeds();
    }
    // 1 strength 104
    // 2 agility 24
    // 3 stamina -56
    // 4 health -136    
    private void SetLevel()
    {
        levelText[0].text = $"{pc.currentLevels[0]}";
        levelText[1].text = $"{pc.currentLevels[1]}";
        levelText[2].text = $"{pc.currentLevels[2]}";
        levelText[3].text = $"{pc.currentLevels[3]}";
    }

    private void SetValue()
    {
        valueText[0].text = $"{pc.strength}";
        valueText[1].text = $"{pc.agility}";
        valueText[2].text = $"{pc.stamina}";
        valueText[3].text = $"{pc.health}";
    }

    private void SetNeeds()
    {

        needText[0].text =pc.currentLevels[0] <4? $"{pc.nextLevels[pc.currentLevels[0]]}":$"{pc.nextLevels[pc.currentLevels[0]-1]}";
        
        needText[1].text = pc.currentLevels[1] <4?$"{pc.nextLevels[pc.currentLevels[1]]}":$"{pc.nextLevels[pc.currentLevels[1]-1]}";
        
        needText[2].text =pc.currentLevels[2] <4? $"{pc.nextLevels[pc.currentLevels[2]]}":$"{pc.nextLevels[pc.currentLevels[2]-1]}";
        
        needText[3].text =pc.currentLevels[3] <4? $"{pc.nextLevels[pc.currentLevels[3]]}":$"{pc.nextLevels[pc.currentLevels[3]-1]}";
    }
}