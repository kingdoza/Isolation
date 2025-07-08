using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    public AudioMixer gameAudioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private const float MinVolume = 0.0001f; 

    void Start()
    {
        
        masterSlider.minValue = MinVolume;
        bgmSlider.minValue = MinVolume;
        sfxSlider.minValue = MinVolume;

        LoadVolumeSettings();
    }

    public void SetMasterVolume()
    {
        float volume = Mathf.Max(masterSlider.value, MinVolume);
        gameAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetBGMVolume()
    {
        float volume = Mathf.Max(bgmSlider.value, MinVolume);
        gameAudioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = Mathf.Max(sfxSlider.value, MinVolume);
        gameAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolumeSettings()
    {
        float masterVolume = Mathf.Max(PlayerPrefs.GetFloat("MasterVolume", 1f), MinVolume);
        float bgmVolume = Mathf.Max(PlayerPrefs.GetFloat("BGMVolume", 1f), MinVolume);
        float sfxVolume = Mathf.Max(PlayerPrefs.GetFloat("SFXVolume", 1f), MinVolume);

        masterSlider.value = masterVolume;
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;

        gameAudioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        gameAudioMixer.SetFloat("BGMVolume", Mathf.Log10(bgmVolume) * 20);
        gameAudioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }
}
