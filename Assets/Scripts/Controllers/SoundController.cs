using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private BGMLibrary bgmClips;



    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }



    public void PlayBGM()
    {
        bgmSource.clip = bgmClips.main;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
