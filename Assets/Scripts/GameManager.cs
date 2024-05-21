using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent, RequireComponent(typeof(InputHandler))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isPaused = false;
    private bool isPauseable = true;

    private InputHandler inputHandler;

    public delegate void pauseDelegate(bool isPaused);
    public event pauseDelegate Pause = null;

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
    }

    private void Start()
    {
        inputHandler = InputHandler.Instance;
    }

    private void Update()
    {
        HandlePauseInput();
    }

    private void OnEnable()
    {
        MenuController.Resume += TogglePause;
    }

    private void OnDisable()
    {
        MenuController.Resume -= TogglePause;
    }

    private void HandlePauseInput()
    {
#if UNITY_EDITOR
        if (SceneManager.GetActiveScene().name == "Menu") return;
#else
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
#endif

        if (inputHandler.pauseTrigger && isPauseable)
        {
            StartCoroutine(Delay(0.3f));
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Pause?.Invoke(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    private IEnumerator Delay(float sec)
    {
        isPauseable = false;
        yield return new WaitForSecondsRealtime(sec);
        isPauseable = true;
    }
}
