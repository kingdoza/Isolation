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
    private const float FadeDuration = 2f;

    private Tween fadeTween;



    private void Awake()
    {
        bgmSource.loop = true;
    }



    public void PlaySFX(AudioClip sfxClip)
    {
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
            // 클립이 null이면 페이드아웃만 (재생중일 때만)
            if (bgmSource.isPlaying)
            {
                fadeTween = bgmSource.DOFade(0f, FadeDuration)
                    .SetUpdate(true)
                    .OnComplete(() => bgmSource.Stop());
            }
            else
            {
                // 재생중 아니면 그냥 정지
                bgmSource.Stop();
                bgmSource.volume = 0f;
            }
            return;
        }

        if (bgmSource.isPlaying)
        {
            // 재생 중이면 페이드아웃 → 페이드인
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
            // 재생중이 아니면 바로 페이드인
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
