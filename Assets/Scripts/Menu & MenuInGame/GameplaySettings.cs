//using System.Collections;
//using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Localization.Settings;
using static PlayerPrefsUtils;

public class GameplaySettings : MonoBehaviour
{
    [Header("Gameplay Settings")]
    [SerializeField] Slider mouseSensitivitySlider;
    [SerializeField] Toggle invertMouseYToggle;
    [SerializeField] TMP_Dropdown languageDropdown;
    [SerializeField] Toggle nightModeToggle; //to be implemented

    // for setting static variables of mouse
    public delegate void GameplaySet(float sensitivity, bool invertY);
    public static event GameplaySet Settings = null;

    void Start()
    {
        SetGameplay();
    }

    void OnEnable()
    {
        mouseSensitivitySlider.onValueChanged.AddListener(delegate
        {
            SetMouseSensitivity(mouseSensitivitySlider.value);
        });
        invertMouseYToggle.onValueChanged.AddListener(delegate
        {
            SetY(invertMouseYToggle.isOn);
        });
        //languageDropdown.onValueChanged.AddListener(delegate
        //{
        //    SetLanguage(languageDropdown.value);
        //});
    }

    void OnDisable()
    {
        mouseSensitivitySlider.onValueChanged.RemoveListener(delegate
        {
            SetMouseSensitivity(mouseSensitivitySlider.value);
        });
        invertMouseYToggle.onValueChanged.RemoveListener(delegate
        {
            SetY(invertMouseYToggle.isOn);
        });
        //languageDropdown.onValueChanged.RemoveListener(delegate
        //{
        //    SetLanguage(languageDropdown.value);
        //});
    }

    //void SetLanguage(int value)
    //{
    //    StartCoroutine(SetLocale(value));
    //    PlayerPrefs.SetInt("Language", value);
    //}

    void SetMouseSensitivity(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        mouseSensitivitySlider.value = value;

        Settings?.Invoke(mouseSensitivitySlider.value, invertMouseYToggle.isOn);
    }

    void SetY(bool value)
    {
        PlayerPrefs.SetInt("InvertY", value ? 1 : 0);
        invertMouseYToggle.isOn = value;

        Settings?.Invoke(mouseSensitivitySlider.value, invertMouseYToggle.isOn);
    }

    void SetGameplay()
    {
        mouseSensitivitySlider.value = GetSavedFloat("ControllerSen");
        invertMouseYToggle.isOn = GetSavedInt("InvertY") == 1;

        //languageDropdown.ClearOptions();
        ////languageDropdown.AddOptions(LocalizationSettings.AvailableLocales.Locales.Select(text => text.LocaleName).ToList());
        //languageDropdown.value = GetSavedInt("Language");
        //StartCoroutine(SetLocale(GetSavedInt("Language")));
        //languageDropdown.RefreshShownValue();

        Settings?.Invoke(mouseSensitivitySlider.value, invertMouseYToggle.isOn);
    }

    //IEnumerator SetLocale(int value)
    //{
    //    yield return LocalizationSettings.InitializationOperation;
    //    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[value];
    //}
}
