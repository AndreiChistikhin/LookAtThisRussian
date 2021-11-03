using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector3 cameraPosition;
    private Vector3 playerPosition;

    void LateUpdate()
    {
       
        cameraPosition = transform.position;
        playerPosition = GameObject.FindObjectOfType<Movement>().PlayerPosition;
        
        cameraPosition.x = playerPosition.x;
        cameraPosition.z = playerPosition.z;
        transform.position = cameraPosition;  
    }
    
}
