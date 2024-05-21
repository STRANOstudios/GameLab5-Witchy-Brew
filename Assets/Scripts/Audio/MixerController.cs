using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class MixerController : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Audio Slider")]
    [SerializeField] Slider masterSlider = null;
    [SerializeField] Slider musicSlider = null;
    [SerializeField] Slider sfxSlider = null;

    private void Start()
    {
        SetSliderVolumes();
    }

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SaveSliderVolumes);
        musicSlider.onValueChanged.AddListener(SaveSliderVolumes);
        sfxSlider.onValueChanged.AddListener(SaveSliderVolumes);
    }

    private void OnDestroy()
    {
        masterSlider.onValueChanged.RemoveListener(SaveSliderVolumes);
        musicSlider.onValueChanged.RemoveListener(SaveSliderVolumes);
        sfxSlider.onValueChanged.RemoveListener(SaveSliderVolumes);
    }

    void SetMixerVolumes()
    {
        mixer.SetFloat("Master", PlayerPrefs.GetFloat(masterSlider.name));

        mixer.SetFloat("Music", PlayerPrefs.GetFloat(musicSlider.name));

        mixer.SetFloat("Sfx", PlayerPrefs.GetFloat(sfxSlider.name));
    }

    public void SetSliderVolumes()
    {
        masterSlider.value = !PlayerPrefs.HasKey(masterSlider.name) ? 0 : PlayerPrefs.GetFloat(masterSlider.name);

        musicSlider.value = !PlayerPrefs.HasKey(musicSlider.name) ? 0 : PlayerPrefs.GetFloat(musicSlider.name);

        sfxSlider.value = !PlayerPrefs.HasKey(sfxSlider.name) ? 0 : PlayerPrefs.GetFloat(sfxSlider.name);

        SetMixerVolumes();
    }

    public void SaveSliderVolumes(float value)
    {
        PlayerPrefs.SetFloat(masterSlider.name, masterSlider.value);
        PlayerPrefs.SetFloat(musicSlider.name, musicSlider.value);
        PlayerPrefs.SetFloat(sfxSlider.name, sfxSlider.value);

        SetMixerVolumes();
    }
}
