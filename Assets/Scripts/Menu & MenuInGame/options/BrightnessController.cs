using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class BrightnessController : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider = null;

    private ColorAdjustments colorAdjustments;
    private int _brightnessLevel;
    private Volume volume = null;

    private void Awake()
    {
        volume = GameObject.Find("Post Processing").GetComponent<Volume>();
        Initialize();
    }

    private void OnEnable()
    {
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    private void OnDestroy()
    {
        brightnessSlider.onValueChanged.RemoveListener(SetBrightness);
    }

    private void SetBrightness(float value)
    {
        _brightnessLevel = (int)value;
        PlayerPrefs.SetInt("Brightness", _brightnessLevel);

        if (!volume.profile.TryGet(out colorAdjustments)) return;

        Color newColor = new(_brightnessLevel / 255f, _brightnessLevel / 255f, _brightnessLevel / 255f, 1);
        colorAdjustments.colorFilter.Override(newColor);
    }

    public void Initialize()
    {
        brightnessSlider.value = PlayerPrefs.GetInt("Brightness") == 0 ? 211 : PlayerPrefs.GetInt("Brightness");
        SetBrightness(brightnessSlider.value);
    }
}