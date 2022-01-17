using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward = (cameraTransform.position - transform.position).normalized;
        transform.forward = cameraTransform.forward;
    }
}
