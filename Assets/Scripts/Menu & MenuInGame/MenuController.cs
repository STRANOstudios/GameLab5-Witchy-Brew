using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels To Load")]
    [SerializeField, Tooltip("The name of the scene to be loaded")] private string sceneToBeLoad;

    [Header("Reference")]
    [SerializeField] private Transform menuButtons;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 10f)] private float secMenuAnim = 10f;

    private float elapsedTime = 0f;

    public delegate void MenuControllerDelegate();
    public static event MenuControllerDelegate Resume;

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

    public void OptionsButton(float angle)
    {
        if (menuButtons)
        {
              StartCoroutine(AnimRotation(angle));
        }
    }

    private IEnumerator AnimRotation(float targetAngle = 180f)
    {
        float startAngle = menuButtons.rotation.eulerAngles.y;

        while (elapsedTime < secMenuAnim)
        {
            float t = elapsedTime / secMenuAnim;
            float angle = Mathf.Lerp(startAngle, targetAngle, t);
            menuButtons.rotation = Quaternion.Euler(0, angle, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        menuButtons.rotation = Quaternion.Euler(0, targetAngle, 0);
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