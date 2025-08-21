using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance;//
    public AudioMixer gameAudioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private const float MinVolume = 0.0001f; 
    private const float MaxVolume = 1.0f; 
    private const float DefaultVolume = 0.75f; 
    //
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
//
    void Start()
    {
        // PlayerPrefs.SetFloat("MasterVolume", DefaultVolume);
        // PlayerPrefs.SetFloat("BGMVolume", DefaultVolume);
        // PlayerPrefs.SetFloat("SFXVolume", DefaultVolume);
        DontDestroyOnLoad(gameObject);
        SetSliderRanges();
        LoadVolumeSettings();
    }

    public void SetMasterVolume()
    {
        float volume = Mathf.Clamp(masterSlider.value, MinVolume, MaxVolume);
        gameAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetBGMVolume()
    {
        float volume = Mathf.Clamp(bgmSlider.value, MinVolume, MaxVolume);
        gameAudioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = Mathf.Clamp(sfxSlider.value, MinVolume, MaxVolume);
        gameAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void SetSliderRanges()
{
    masterSlider.minValue = MinVolume;
    masterSlider.maxValue = MaxVolume;

    bgmSlider.minValue = MinVolume;
    bgmSlider.maxValue = MaxVolume;

    sfxSlider.minValue = MinVolume;
    sfxSlider.maxValue = MaxVolume;
}
    private void LoadVolumeSettings()
    {
        float masterVolume = Mathf.Clamp(PlayerPrefs.GetFloat("MasterVolume", DefaultVolume), MinVolume, MaxVolume);
        float bgmVolume = Mathf.Clamp(PlayerPrefs.GetFloat("BGMVolume", DefaultVolume), MinVolume, MaxVolume);
        float sfxVolume = Mathf.Clamp(PlayerPrefs.GetFloat("SFXVolume", DefaultVolume), MinVolume, MaxVolume);

        masterSlider.value = DefaultVolume;
        bgmSlider.value = DefaultVolume;
        sfxSlider.value = DefaultVolume;

        gameAudioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value) * 20);
        gameAudioMixer.SetFloat("BGMVolume", Mathf.Log10(bgmSlider.value) * 20);
        gameAudioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
    }
}
