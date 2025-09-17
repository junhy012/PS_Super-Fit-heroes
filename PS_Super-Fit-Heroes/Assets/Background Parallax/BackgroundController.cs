using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos;
    public GameObject cam;

    //The speed at which the background should be relative to camera
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if 0, move with camera
        // if 1, won't move
        // if 0.5 half
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

    }
}
