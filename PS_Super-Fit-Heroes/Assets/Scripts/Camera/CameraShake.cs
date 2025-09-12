using System.Collections;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    
    public bool isShake = false;
   
    public IEnumerator ShakeCamera(float duration = 0.2f, float magnitude = 0.05f)
    {
        isShake = true;
        Vector3 originalPos = transform.position;

        float elaspedTime = 0;

        while (elaspedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = originalPos + new Vector3(x, y, 0);
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
        isShake = false;
    } 
}
