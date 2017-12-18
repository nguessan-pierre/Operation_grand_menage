using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetCamPos = target.position + new Vector3(0, 30, -20);
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);  
        }
    }
}