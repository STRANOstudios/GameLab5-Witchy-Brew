using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Rederences")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Cation Name Refernces")]
    [SerializeField] private string pause = "Pause";

    private InputAction pauseAction;
    public bool pauseTrigger { get; private set; }

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

        pauseAction = playerControls.FindActionMap(actionMapName).FindAction(pause);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        pauseAction.performed += context => pauseTrigger = true;
        pauseAction.canceled += context => pauseTrigger = false;
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }
}