using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    AutoExposure exposure;

    void Start()
    {
        brightness.TryGetSettings(out exposure);

        if (!PlayerPrefs.HasKey("Brightness"))
        {
            PlayerPrefs.SetFloat("Brightness", 0.5f);
            PlayerPrefs.Save();
        }

        LoadBrightness();
    }

    public void AdjustBrightness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = 0.05f;
        }

        SaveBrightness(value);
    }

    private void SaveBrightness(float value)
    {
        PlayerPrefs.SetFloat("Brightness", value);
        PlayerPrefs.Save();    }

    private void LoadBrightness()
    {
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 0.5f);
        brightnessSlider.value = savedBrightness;
        AdjustBrightness(savedBrightness);
    }
}
