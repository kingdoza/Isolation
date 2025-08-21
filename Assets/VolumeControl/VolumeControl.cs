using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static GameData;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance;//
    public AudioMixer gameAudioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private const float MinVolume = 0.0001f; 
    private const float MaxVolume = 1.0f; 
    private const float DefaultVolume = 0.5f;

    private const float VolumeRate = 20f;
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



    public void SetVolumes()
    {
        Debug.Log("SetVolumes");
        gameAudioMixer.SetFloat("MasterVolume", ConvertToDecibel(MasterVolume));
        gameAudioMixer.SetFloat("BGMVolume", ConvertToDecibel(BGMVolume));
        gameAudioMixer.SetFloat("SFXVolume", ConvertToDecibel(SFXVolume));
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
        //transform.parent.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        LoadVolumeSettings();
    }

    public void SetMasterVolume()
    {
        MasterVolume = Mathf.Clamp(masterSlider.value, MinVolume, MaxVolume);
        gameAudioMixer.SetFloat("MasterVolume", ConvertToDecibel(MasterVolume));
        //PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetBGMVolume()
    {
        BGMVolume = Mathf.Clamp(bgmSlider.value, MinVolume, MaxVolume);
        gameAudioMixer.SetFloat("BGMVolume", ConvertToDecibel(BGMVolume));
        //PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume()
    {
        SFXVolume = Mathf.Clamp(sfxSlider.value, MinVolume, MaxVolume);
        gameAudioMixer.SetFloat("SFXVolume", ConvertToDecibel(SFXVolume));
        //PlayerPrefs.SetFloat("SFXVolume", volume);
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
        //float masterVolume = Mathf.Clamp(PlayerPrefs.GetFloat("MasterVolume", DefaultVolume), MinVolume, MaxVolume);
        //float bgmVolume = Mathf.Clamp(PlayerPrefs.GetFloat("BGMVolume", DefaultVolume), MinVolume, MaxVolume);
        //float sfxVolume = Mathf.Clamp(PlayerPrefs.GetFloat("SFXVolume", DefaultVolume), MinVolume, MaxVolume);

        masterSlider.value = MasterVolume;
        bgmSlider.value = BGMVolume;
        sfxSlider.value = SFXVolume;
        SetVolumes();

        //gameAudioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value) * 20);
        //gameAudioMixer.SetFloat("BGMVolume", Mathf.Log10(bgmSlider.value) * 20);
        //gameAudioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
    }



    public float ConvertToDecibel(float value)
    {
        if (value <= 0.0001f)
            return -80f; // mute

        // 0.5°¡ ±âÁØ ¡æ 0 dB
        return 20f * Mathf.Log10(value / 0.5f);
    }
}
