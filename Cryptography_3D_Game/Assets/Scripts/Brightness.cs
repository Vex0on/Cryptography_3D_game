using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    AutoExposure exposure;

    [System.Obsolete]
    void Start()
    {
        brightness.TryGetSettings(out exposure);

        Brightness[] existingScripts = GameObject.FindObjectsOfType<Brightness>();
        if (existingScripts.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            GameObject parentObject = GameObject.Find("BrightnessParent");
            if (parentObject == null)
            {
                parentObject = new GameObject("BrightnessParent");
                DontDestroyOnLoad(parentObject);
                transform.SetParent(parentObject.transform);
            }

            LoadBrightness();
        }
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
    }

    private void LoadBrightness()
    {
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 0.5f);
        brightnessSlider.value = savedBrightness;
        AdjustBrightness(savedBrightness);
    }
}
