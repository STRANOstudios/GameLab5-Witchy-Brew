using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class ResolutionController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown = null;
    private Resolution[] resolutions;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void OnDestroy()
    {
        resolutionDropdown.onValueChanged.RemoveListener(SetResolution);
    }

    private void SetResolution(int value)
    {
        Resolution resolution = Screen.resolutions[value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
    }

    public void Initialize()
    {
        Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth", Screen.width), PlayerPrefs.GetInt("ResolutionHeight", Screen.height), Screen.fullScreen);

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
}