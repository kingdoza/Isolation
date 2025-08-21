using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private BGMLibrary bgmClips;
    public static float BGM_Volume = 0.5f;
    public static float SFX_Volume = 0.6f;
    private const float FadeDuration = 1.8f;
    public static SoundController instance;
    public static SoundController Instance => instance;

    private Tween fadeTween;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    public AudioMixerGroup SFX_MixerGroup => sfxMixerGroup;



    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //    instance = this;
    //    DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //    Destroy(gameObject);
    //    return;
    //    }

    //    // bgmSource.loop = true; 
    //    if (bgmSource != null)
    //    {
    //    bgmSource.loop = true;
    //    bgmSource.playOnAwake = false; 
    //    }
    //    if (sfxSource != null)
    //    {
    //    sfxSource.playOnAwake = false;
    //    }


    //    if (bgmClips != null && bgmClips.main!= null && !bgmSource.isPlaying)
    //    {
    //    PlayBGM(bgmClips.main);
    //    }
    //}



    private void Awake()
    {
        bgmSource.loop = true;
    }



    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip == null)
        {
            //sfxSource.Stop();
            return;
        }
        sfxSource.volume = SFX_Volume;
        sfxSource.PlayOneShot(sfxClip);
    }



    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmClip == null)
        {
            bgmSource.Stop();
            return;
        }
        bgmSource.volume = BGM_Volume;
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }



    public void CrossFadeBGM(AudioClip bgmClip)
    {
        fadeTween?.Kill();

        if (bgmClip == null)
        {
            // Ŭ���� null�̸� ���̵�ƿ��� (������� ����)
            if (bgmSource.isPlaying)
            {
                fadeTween = bgmSource.DOFade(0f, FadeDuration)
                    .SetUpdate(true)
                    .OnComplete(() => bgmSource.Stop());
            }
            else
            {
                // ����� �ƴϸ� �׳� ����
                bgmSource.Stop();
                bgmSource.volume = 0f;
            }
            return;
        }

        if (bgmSource.isPlaying)
        {
            // ��� ���̸� ���̵�ƿ� �� ���̵���
            fadeTween = DOTween.Sequence()
                .Append(bgmSource.DOFade(0f, FadeDuration).SetUpdate(true))
                .AppendCallback(() =>
                {
                    bgmSource.Stop();
                    bgmSource.clip = bgmClip;
                    bgmSource.volume = 0f;
                    bgmSource.Play();
                })
                .Append(bgmSource.DOFade(BGM_Volume, FadeDuration).SetUpdate(true));
        }
        else
        {
            // ������� �ƴϸ� �ٷ� ���̵���
            bgmSource.Stop();
            bgmSource.clip = bgmClip;
            bgmSource.volume = 0f;
            bgmSource.Play();

            fadeTween = bgmSource.DOFade(BGM_Volume, FadeDuration).SetUpdate(true);
        }
    }



    public void FadeOutBGM(float fadeDuration)
    {
        fadeTween?.Kill();

        if (!bgmSource.isPlaying)
        {
            bgmSource.volume = 0;
            return;
        }

        fadeTween = bgmSource.DOFade(0f, fadeDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                bgmSource.Stop();
            });
    }



    public void FadeInBGM(AudioClip bgmClip, float fadeDuration)
    {
        fadeTween?.Kill();

        bgmSource.volume = 0;
        bgmSource.clip = bgmClip;
        bgmSource.Play();

        Debug.Log("fadeDuration : " + fadeDuration);
        fadeTween = bgmSource.DOFade(BGM_Volume, fadeDuration)
            .SetUpdate(true);
    }

    
}


