using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static PlayerPrefsUtils;

public class GraphicsSettings : MonoBehaviour
{
    [Header("Graphics Settings")]
    [SerializeField] Slider brightnessSlider = null;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullScreenToggle = null;
    [SerializeField] Volume volume = null;

    private Resolution[] resolutions;

    private bool _isFullScreen;
    private int _brightnessLevel;
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        SetGraphics();
        SetResolutionDropdown();
    }

    void OnEnable()
    {
        brightnessSlider.onValueChanged.AddListener(delegate
        {
            SetBrightness(brightnessSlider.value);
        });
        resolutionDropdown.onValueChanged.AddListener(delegate
        {
            SetResolution(resolutionDropdown.value);
        });
        fullScreenToggle.onValueChanged.AddListener(delegate
        {
            SetFullScreen(fullScreenToggle.isOn);
        });
    }

    void OnDisable()
    {
        brightnessSlider.onValueChanged.RemoveListener(delegate
        {
            SetBrightness(brightnessSlider.value);
        });
        resolutionDropdown.onValueChanged.RemoveListener(delegate
        {
            SetResolution(resolutionDropdown.value);
        });
        fullScreenToggle.onValueChanged.RemoveListener(delegate
        {
            SetFullScreen(fullScreenToggle.isOn);
        });
    }

    void SetResolutionDropdown()
    {
        resolutions = Screen.resolutions;

        List<Resolution> tmp = new();
        List<string> options = new();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;

            if (options.Contains(option)) continue;

            tmp.Add(resolutions[i]);

            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        Array.Resize(ref resolutions, tmp.Count);
        resolutions = tmp.ToArray();

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    void SetBrightness(float value)
    {
        _brightnessLevel = (int)value;
        PlayerPrefs.SetInt("Brightness", _brightnessLevel);

        if (!volume.profile.TryGet(out colorAdjustments)) return;

        Color newColor = new(_brightnessLevel / 255f, _brightnessLevel / 255f, _brightnessLevel / 255f, 1);
        colorAdjustments.colorFilter.Override(newColor);
    }

    void SetResolution(int value)
    {
        Resolution resolution = resolutions[value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetFloat("Resolution_width", resolution.width);
        PlayerPrefs.SetFloat("Resolution_height", resolution.height);
    }

    void SetFullScreen(bool value)
    {
        _isFullScreen = value;
        Screen.fullScreen = _isFullScreen;
        PlayerPrefs.SetInt("FullScreen", (_isFullScreen ? 0 : 1));
    }

    void SetGraphics()
    {
        brightnessSlider.value = GetSavedInt("Brightness") == 0 ? 211 : GetSavedInt("Brightness");

        bool _isFullScreen = GetSavedInt("FullScreen") == 0;

        fullScreenToggle.isOn = !_isFullScreen;
        Screen.SetResolution((int)GetSavedFloat("Resolution_width"), (int)GetSavedFloat("Resolution_height"), _isFullScreen);
    }
}
