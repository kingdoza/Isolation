using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;

public class Intro : MonoBehaviour
{
    [SerializeField] private string[] phrases;
    [SerializeField] private CanvasGroup phrasePanel;
    [SerializeField] private TextMeshProUGUI phraseBox;
    [SerializeField] private float fadeinDuration;
    [SerializeField] private float fadeoutDuration;
    [SerializeField] private float holdDuration;
    [SerializeField] private float playDuration;
    private int currentPhraseIdx = 0;



    private void Start()
    {
        phrasePanel.alpha = 0f;
        DG.Tweening.Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2.5f);
        seq.OnComplete(PlayPhraseSequence);
    }



    private void SkipNextPhrase()
    {
        ++currentPhraseIdx;
        if (currentPhraseIdx >= phrases.Length)
        {
            FinishSequence();
            return;
        }
        PlayPhraseSequence();
    }



    private void PlayPhraseSequence()
    {
        if (currentPhraseIdx >= phrases.Length - 1)
            holdDuration += 1;
        phrasePanel.alpha = 0;
        //phraseBox.text = phrases[currentPhraseIdx];
        ApplyPhraseText();
        DG.Tweening.Sequence seq = DOTween.Sequence();
        seq.Append(phrasePanel.DOFade(1f, fadeinDuration));
        seq.AppendInterval(holdDuration);
        seq.Append(phrasePanel.DOFade(0f, fadeoutDuration));
        seq.AppendInterval(playDuration);
        seq.OnComplete(SkipNextPhrase);
        seq.Play();
    }



    private void ApplyPhraseText() 
    {
        string targetPhrase = phrases[currentPhraseIdx];
        if (targetPhrase.Length >= 2 && currentPhraseIdx <= 2)
        {
            string firstPart = targetPhrase.Substring(0, targetPhrase.Length - 2);
            string lastTwo = targetPhrase.Substring(targetPhrase.Length - 2);
            string colorFormat = "";
            switch (currentPhraseIdx)
            {
                case 0:
                    colorFormat = "<color=#D0A38C>"; // 과거, 더 연한 브라운
                    break;
                case 1:
                    colorFormat = "<color=#8FD18F>"; // 현재, 더 연한 녹색
                    break;
                case 2:
                    colorFormat = "<color=#B19ED8>"; // 미래, 더 연한 보라
                    break;
            }
            phraseBox.text = firstPart + colorFormat + lastTwo + "</color>";
        }
        else if (currentPhraseIdx == 3)
        {
            string colored = targetPhrase.Replace("고립", "<color=#FF0000>고립</color>");
            phraseBox.text = colored;
        }
        else
        {
            phraseBox.text = targetPhrase;
        }

        if (currentPhraseIdx == phrases.Length - 1)
        {
            phraseBox.fontSize = 60;
            DOTween.To(
                () => phraseBox.fontSize,
                x => phraseBox.fontSize = x,
                65,
                fadeoutDuration + fadeinDuration + holdDuration
            ).SetEase(Ease.Linear);
        }
    }



    private void FinishSequence()
    {
        Debug.Log("wewe");
        //SceneManager.LoadScene("MainScene");
        SceneManager.LoadScene("Refactor");
    }
}
