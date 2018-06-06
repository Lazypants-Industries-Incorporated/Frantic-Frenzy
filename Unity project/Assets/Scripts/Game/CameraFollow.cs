using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public static int lockCamera; //Used in PressurePlate script
    public GameObject player;

    private Vector3 offset;


    void Start()
    {
        lockCamera = 0;
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (lockCamera == 0) //If camera is not locked, then change position
            transform.position = player.transform.position + offset;
    }
}
