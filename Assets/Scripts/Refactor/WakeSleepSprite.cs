using UnityEngine;

public class WakeSleepSprite : WakeSleepBranch
{
    [SerializeField] private Sprite wakeSprite;
    [SerializeField] private Sprite sleepSprite;



    protected override void SetSleepComponent()
    {
        GetComponent<SpriteRenderer>().sprite = sleepSprite;
    }



    protected override void SetWakeupComponent()
    {
        GetComponent<SpriteRenderer>().sprite = wakeSprite;
    }
}
