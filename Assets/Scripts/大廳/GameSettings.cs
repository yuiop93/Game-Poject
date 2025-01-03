using UnityEngine;
using System;

public static class GameSettings
{
    public static readonly Resolution[] AvailableResolutions =
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1280, height = 720 },
    };

    public static Resolution CurrentResolution { get; private set; }
    public static bool IsFullscreen { get; private set; }
    public static int TargetFrameRate { get; private set; }

    // 音量和靈敏度
    public static float Sensitivity { get; private set; }
    public static float MasterVolume { get; private set; }
    public static float SFXVolume { get; private set; }
    public static float MusicVolume { get; private set; }

    // 陰影品質
    public static int ShadowQuality { get; private set; }

    public static void Initialize()
    {
        // 讀取設定
        string savedResolution = PlayerPrefs.GetString("Resolution", $"{AvailableResolutions[0].width}x{AvailableResolutions[0].height}");
        string[] resParts = savedResolution.Split('x');
        CurrentResolution = new Resolution { width = int.Parse(resParts[0]), height = int.Parse(resParts[1]) };

        IsFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.SetResolution(CurrentResolution.width, CurrentResolution.height, IsFullscreen);

        TargetFrameRate = PlayerPrefs.GetInt("FrameRate", 60);
        Application.targetFrameRate = TargetFrameRate;

        Sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.0f);
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        AudioListener.volume = MasterVolume;

        ShadowQuality = PlayerPrefs.GetInt("ShadowQuality", 2);  // Medium by default
        SetShadowQuality(ShadowQuality);
    }

    public static void SetResolution(Resolution resolution)
    {
        CurrentResolution = resolution;
        Screen.SetResolution(resolution.width, resolution.height, IsFullscreen);
        PlayerPrefs.SetString("Resolution", $"{resolution.width}x{resolution.height}");
        PlayerPrefs.Save();
    }

    public static void SetFullscreen(bool isFullscreen)
    {
        IsFullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void SetFrameRate(int frameRate)
    {
        TargetFrameRate = frameRate;
        Application.targetFrameRate = frameRate;
        PlayerPrefs.SetInt("FrameRate", frameRate);
        PlayerPrefs.Save();
    }

    public static void SetSensitivity(float sensitivity)
    {
        Sensitivity = Mathf.Clamp(sensitivity, 0.5f, 2.0f);
        PlayerPrefs.SetFloat("Sensitivity", Sensitivity);
        PlayerPrefs.Save();
    }

    public static void SetMasterVolume(float volume)
    {
        MasterVolume = Mathf.Clamp01(volume);
        AudioListener.volume = MasterVolume;
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.Save();
    }

    public static void SetSFXVolume(float volume)
    {
        SFXVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        PlayerPrefs.Save();
    }

    public static void SetMusicVolume(float volume)
    {
        MusicVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.Save();
    }
    public static event System.Action<int> OnShadowQualityChanged;
    public static void SetShadowQuality(int quality)
    {
        ShadowQuality = quality;

        QualitySettings.shadowCascades = (quality == 0) ? 0 : 4;
        QualitySettings.shadowResolution = (quality == 0) ? ShadowResolution.Low : (quality == 1 ? ShadowResolution.Medium : ShadowResolution.High);

        PlayerPrefs.SetInt("ShadowQuality", ShadowQuality);
        PlayerPrefs.Save();

        OnShadowQualityChanged?.Invoke(quality); // 通知所有訂閱者
    }
    
}
