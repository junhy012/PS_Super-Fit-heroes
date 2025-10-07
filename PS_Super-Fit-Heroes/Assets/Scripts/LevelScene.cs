using System;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    public GameObject spawnPoint;
    private void Awake()
    {
        GameObject go = Instantiate(GameManager.instance.playerCharacter, spawnPoint.transform.position, Quaternion.identity);
        Camera.main.transform.GetComponent<CameraFollow>().target = go.transform;
    }
}
