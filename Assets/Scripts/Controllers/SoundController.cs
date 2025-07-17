using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip lightSwitch;

    public AudioClip Click => click;
    public AudioClip LightSwitch => lightSwitch;



    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }
}
