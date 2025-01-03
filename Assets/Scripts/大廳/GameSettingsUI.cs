using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown frameRateDropdown;
    public Toggle fullscreenToggle;
    public Slider sensitivitySlider;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    private void Awake()
    {
        GameSettings.Initialize();
        InitializeUI();
    }

    private void InitializeUI()
    {
        // 初始化解析度選項
        resolutionDropdown.ClearOptions();
        foreach (var res in GameSettings.AvailableResolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData($"{res.width} x {res.height}"));
        }

        // 設定解析度預設值
        for (int i = 0; i < GameSettings.AvailableResolutions.Length; i++)
        {
            if (GameSettings.AvailableResolutions[i].width == GameSettings.CurrentResolution.width &&
                GameSettings.AvailableResolutions[i].height == GameSettings.CurrentResolution.height)
            {
                resolutionDropdown.value = i;
                break;
            }
        }

        // 初始化幀數選項
        frameRateDropdown.ClearOptions();
        frameRateDropdown.AddOptions(new System.Collections.Generic.List<string> { "30", "60", "120" });
        frameRateDropdown.value = GameSettings.TargetFrameRate == 120 ? 2 : GameSettings.TargetFrameRate == 60 ? 1 : 0;

        // 初始化其他選項
        fullscreenToggle.isOn = GameSettings.IsFullscreen;
        sensitivitySlider.value = GameSettings.Sensitivity;
        masterVolumeSlider.value = GameSettings.MasterVolume;
        sfxVolumeSlider.value = GameSettings.SFXVolume;
        musicVolumeSlider.value = GameSettings.MusicVolume;

        // 註冊事件
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        frameRateDropdown.onValueChanged.AddListener(OnFrameRateChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggled);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    public void OnResolutionChanged(int index)
    {
        GameSettings.SetResolution(GameSettings.AvailableResolutions[index]);

        // 確保解析度 Dropdown 顯示正確的文字
        resolutionDropdown.captionText.text = $"{GameSettings.CurrentResolution.width} x {GameSettings.CurrentResolution.height}";
    }

    public void OnFrameRateChanged(int index)
    {
        int[] frameRates = { 30, 60, 120 };
        GameSettings.SetFrameRate(frameRates[index]);
    }

    public void OnFullscreenToggled(bool isFullscreen)
    {
        GameSettings.SetFullscreen(isFullscreen);
    }

    public void OnSensitivityChanged(float value)
    {
        GameSettings.SetSensitivity(value);
    }

    public void OnMasterVolumeChanged(float value)
    {
        GameSettings.SetMasterVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        GameSettings.SetSFXVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        GameSettings.SetMusicVolume(value);
    }

    public void ResetSettings()
    {
        GameSettings.SetSensitivity(1.0f);
        GameSettings.SetMasterVolume(1.0f);
        GameSettings.SetSFXVolume(1.0f);
        GameSettings.SetMusicVolume(1.0f);
        GameSettings.SetResolution(GameSettings.AvailableResolutions[0]);
        GameSettings.SetFrameRate(60);
        GameSettings.SetFullscreen(true);

        // 更新 UI
        InitializeUI();

        // 確保解析度 Dropdown 顯示正確的文字
        resolutionDropdown.captionText.text = $"{GameSettings.CurrentResolution.width} x {GameSettings.CurrentResolution.height}";
    }
}
