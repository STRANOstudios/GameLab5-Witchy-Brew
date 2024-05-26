using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset systemControls;

    [Header("Action Map Name Rederences")]
    [SerializeField] private string actionMapName = "Player";
    [SerializeField] private string actionMapName2 = "System";

    [Header("Cation Name Refernces")]
    [SerializeField] private string pause = "Pause";
    [SerializeField] private string zoom = "Zoom";

    private InputAction pauseAction;
    private InputAction zoomAction;
    public bool pauseTrigger { get; private set; }
    public Vector2 ZoomInput { get; private set; }

    private void Awake()
    {
        #region Singleton

        if (Instance != null)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        #endregion

        pauseAction = systemControls.FindActionMap(actionMapName2).FindAction(pause);
        zoomAction = systemControls.FindActionMap(actionMapName).FindAction(zoom);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        pauseAction.performed += context => pauseTrigger = true;
        pauseAction.canceled += context => pauseTrigger = false;

        zoomAction.performed += context => ZoomInput = context.ReadValue<Vector2>();
        zoomAction.canceled += context => ZoomInput = Vector2.zero;
    }

    private void OnEnable()
    {
        pauseAction.Enable();
        zoomAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
        zoomAction.Disable();
    }
}