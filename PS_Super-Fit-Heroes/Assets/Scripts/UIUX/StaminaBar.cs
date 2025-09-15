using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    
    private PlayerController pc;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        slider.maxValue = pc.maxStamina;
        slider.value = pc.currentStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.maxValue != pc.maxStamina)
            slider.maxValue = pc.maxStamina;
        
        slider.value = pc.currentStamina;
    }
}
