using Cinemachine;
using UnityEngine;

[
    DisallowMultipleComponent,
    RequireComponent(typeof(CinemachineVirtualCamera))
]
public class Zoom : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float mZoomSpeed = 10f;

    [Header("Settings: FieldOfView")]
    [SerializeField] float fieldOfViewMax = 80f;
    [SerializeField] float fieldOfViewMin = 5f;

    [Header("Settings: MoveForward")]
    [SerializeField] float moveForwardMax = 80f;
    [SerializeField] float moveForwardMin = 5f;

    CinemachineVirtualCamera CinemachineVirtualCamera;
    float targetFieldOfView;

    Vector3 followOffset;

    private InputHandler inputHandler;

    private void Awake()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        followOffset = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Start()
    {
        inputHandler = InputHandler.Instance;
    }

    private void Update()
    {
        //CameraZoom_FieldOfView();
        CameraZoom_MoveForward();
    }

    private void CameraZoom_FieldOfView()
    {
        float fieldOfViewIncreaseAmount = 5f;
        if (inputHandler.ZoomInput.y > 0)
        {
            targetFieldOfView -= fieldOfViewIncreaseAmount;
        }
        else if (inputHandler.ZoomInput.y < 0)
        {
            targetFieldOfView += fieldOfViewIncreaseAmount;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

        CinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(CinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * mZoomSpeed);
    }

    private void CameraZoom_MoveForward()
    {
        Vector3 zoomDir = followOffset.normalized;

        float zoomAmount = 2f;

        if (inputHandler.ZoomInput.y > 0)
        {
            followOffset -= zoomDir * zoomAmount;
        }
        if (inputHandler.ZoomInput.y < 0)
        {
            followOffset += zoomDir * zoomAmount;
        }

        if (followOffset.magnitude < moveForwardMin)
        {
            followOffset = zoomDir * moveForwardMin;
        }
        if (followOffset.magnitude > moveForwardMax)
        {
            followOffset = zoomDir * moveForwardMax;
        }

        CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * mZoomSpeed); ;
    }
}
