using Unity.VisualScripting;
using UnityEngine;

public class Obstacle_On_hit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        
    }


    
    // Update is called once per frame
    void Update()
    {
        
    }
}
