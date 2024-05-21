using UnityEngine;

[DisallowMultipleComponent]
public class LookAtCameraScript : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 lookAtPosition = _camera.transform.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);
    }
}