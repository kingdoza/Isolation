using DG.Tweening;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private BGMLibrary bgmClips;
    private const float BGM_Volume = 0.5f;
    private const float SFX_Volume = 0.6f;
    private const float FadeDuration = 1.5f;
    public static SoundController instance;
    public static SoundController Instance => instance;

    private Tween fadeTween;



    private void Awake()
    {
        if (instance == null)
        {
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else
        {
        Destroy(gameObject);
        return;
        }

        // bgmSource.loop = true; 
        if (bgmSource != null)
        {
        bgmSource.loop = true;
        bgmSource.playOnAwake = false; 
        }
        if (sfxSource != null)
        {
        sfxSource.playOnAwake = false;
        }

        
        if (bgmClips != null && bgmClips.main!= null && !bgmSource.isPlaying)
        {
        PlayBGM(bgmClips.main);
        }
    }



    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip == null) return;
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



    public void FadeOutBGM()
    {
        fadeTween?.Kill();

        if (!bgmSource.isPlaying)
        {
            bgmSource.volume = 0;
            return;
        }

        fadeTween = bgmSource.DOFade(0f, FadeDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                bgmSource.Stop();
            });
    }



    public void FadeInBGM(AudioClip bgmClip)
    {
        fadeTween?.Kill();

        bgmSource.volume = 0;
        bgmSource.clip = bgmClip;
        bgmSource.Play();

        fadeTween = bgmSource.DOFade(0f, BGM_Volume)
            .SetUpdate(true);
    }

    
}


