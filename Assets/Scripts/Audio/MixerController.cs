using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Audio Slider")]
    [SerializeField] Slider masterSlider = null;
    [SerializeField] Slider musicSlider = null;
    [SerializeField] Slider sfxSlider = null;

    private readonly string masterVolume = "Master";
    private readonly string musicVolume = "Music";
    private readonly string sfxVolume = "SFX";

    private void Start()
    {
        SetMixerVolumes();
        SetSliderVolumes();
    }

    void SetMixerVolumes()
    {
        mixer.SetFloat(masterVolume, GetSavedFloat(masterVolume));

        mixer.SetFloat(musicVolume, GetSavedFloat(musicVolume));

        mixer.SetFloat(sfxVolume, GetSavedFloat(sfxVolume));
    }

    public void SetSliderVolumes()
    {
        masterSlider.value = GetSavedFloat(masterVolume);

        musicSlider.value = GetSavedFloat(musicVolume);

        sfxSlider.value = GetSavedFloat(sfxVolume);
    }

    public void SetVolume(Slider slider)
    {
        mixer.SetFloat(slider.name, slider.value);

        PlayerPrefs.SetFloat($"{slider.name}", slider.value);
        PlayerPrefs.Save();
    }

    public void SetSavedFloat(Slider slider)
    {
        mixer.SetFloat(slider.name, slider.value);

        PlayerPrefs.SetFloat($"{slider.name}", slider.value);
        PlayerPrefs.Save();
    }

    float GetSavedFloat(string key)
    {
        if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetFloat(key);
        return 0f;
    }
}
