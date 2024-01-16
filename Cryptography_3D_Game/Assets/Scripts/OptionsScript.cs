using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public Toggle vsyncToggle;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        InitializeSettings();

        vsyncToggle.onValueChanged.AddListener(OnVSyncToggleChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        resolutionDropdown.onValueChanged.AddListener(OnResolutionDropdownChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
    }

    void InitializeSettings()
    {
        vsyncToggle.isOn = PlayerPrefs.GetInt("VSync", 1) == 1;
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        InitializeResolutionDropdown();

        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    void InitializeResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add(resolutions[i].width + " x " + resolutions[i].height);
        }

        resolutionDropdown.AddOptions(resolutionOptions);

        int currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", FindCurrentResolutionIndex());
        resolutionDropdown.value = currentResolutionIndex;

        ApplyResolution(currentResolutionIndex);
    }

    int FindCurrentResolutionIndex()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                return i;
            }
        }

        return 0;
    }

    public void OnVSyncToggleChanged(bool value)
    {
        QualitySettings.vSyncCount = value ? 0 : 1;
        PlayerPrefs.SetInt("VSync", value ? 0 : 1);
    }

    public void OnVolumeSliderChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void OnResolutionDropdownChanged(int index)
    {
        ApplyResolution(index);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }

    public void ApplyResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void OnFullscreenToggleChanged(bool value)
    {
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("Fullscreen", value ? 0 : 1);
    }
}
