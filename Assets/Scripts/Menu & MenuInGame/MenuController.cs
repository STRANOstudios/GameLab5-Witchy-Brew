using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MenuController : MonoBehaviour
{
    [Header("Levels To Load")]
    [SerializeField, Tooltip("The name of the scene to be loaded")] private string sceneToBeLoad;

    public delegate void resumeDelegate();
    public static event resumeDelegate Resume;

    #region Menu Buttons

    public void PlayButton()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(sceneToBeLoad);
#else
        SceneManager.LoadScene(1);
#endif
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ReturnButton()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(sceneToBeLoad);
#else
        SceneManager.LoadScene(0);
#endif
    }

    public void ResumeButton()
    {
        Resume?.Invoke();
    }

    #endregion
}