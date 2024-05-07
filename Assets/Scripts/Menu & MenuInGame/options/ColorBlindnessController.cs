using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[DisallowMultipleComponent]
public class ColorBlindnessController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown colorBlindnessDropdown = null;
    [SerializeField] List<ColorBlindness> colorBlindnessModes = new();

    private Volume volume = null;
    private ChannelMixer channelMixer = null;

    private void Awake()
    {
        volume = GameObject.Find("Post Processing").GetComponent<Volume>();
        Initialize();
    }

    private void OnEnable()
    {
        colorBlindnessDropdown.onValueChanged.AddListener(SetColorBlindness);
    }

    private void OnDestroy()
    {
        colorBlindnessDropdown.onValueChanged.RemoveListener(SetColorBlindness);
    }

    private void SetColorBlindness(int value)
    {
        PlayerPrefs.SetInt("ColorBlindness", value);

        if (!volume.profile.TryGet(out channelMixer)) return;

        channelMixer.redOutBlueIn.Override(colorBlindnessModes[value].redChannel.Blue);
        channelMixer.redOutGreenIn.Override(colorBlindnessModes[value].redChannel.Green);
        channelMixer.redOutRedIn.Override(colorBlindnessModes[value].redChannel.Red);

        channelMixer.blueOutBlueIn.Override(colorBlindnessModes[value].blueChannel.Blue);
        channelMixer.blueOutGreenIn.Override(colorBlindnessModes[value].blueChannel.Green);
        channelMixer.blueOutRedIn.Override(colorBlindnessModes[value].blueChannel.Red);

        channelMixer.greenOutBlueIn.Override(colorBlindnessModes[value].greenChannel.Blue);
        channelMixer.greenOutGreenIn.Override(colorBlindnessModes[value].greenChannel.Green);
        channelMixer.greenOutRedIn.Override(colorBlindnessModes[value].greenChannel.Red);
    }

    public void Initialize()
    {
        List<string> options = new();

        int index = PlayerPrefs.GetInt("ColorBlindness");

        for (int i = 0; i < colorBlindnessModes.Count; i++)
        {
            options.Add(colorBlindnessModes[i].name);
        }

        colorBlindnessDropdown.ClearOptions();
        colorBlindnessDropdown.AddOptions(options);
        colorBlindnessDropdown.value = index;
        colorBlindnessDropdown.RefreshShownValue();

        SetColorBlindness(index);
    }
}