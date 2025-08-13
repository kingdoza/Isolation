using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSetting : MonoBehaviour
{
    public Slider masterSlider;
    public TextMeshProUGUI masterVolumeText;


    void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
    }
    public void SetMasterVolume(float value)
    {
        masterVolumeText.text = $"Master Volume: {(int)(value * 100)}%";
    }
}
