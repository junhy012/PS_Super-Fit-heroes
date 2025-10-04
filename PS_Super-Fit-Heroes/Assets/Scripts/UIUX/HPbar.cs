using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Slider slider;
    
    private PlayerController pc;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        slider.maxValue = pc.maxHp;
        slider.value = pc.currentHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.maxValue != pc.maxHp)
            slider.maxValue = pc.maxHp;
        
        slider.value = pc.currentHp;
    }
}
