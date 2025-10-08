using System;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private void Awake()
    {
        GameObject go = GameObject.FindWithTag("Player");

        if (go == null)
            go = Instantiate(GameManager.instance.playerCharacter, transform.position, Quaternion.identity);
        else
            go.transform.position = transform.position;


        go.GetComponent<PlayerController>().levelScene = this.transform;
        Camera.main.transform.GetComponent<CameraFollow>().target = go.transform;
    }
}