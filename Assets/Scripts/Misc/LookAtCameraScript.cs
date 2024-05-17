using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraScript : MonoBehaviour
{
    private Camera _camera;
    
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 lookAtPosition = _camera.transform.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);
    }
}
