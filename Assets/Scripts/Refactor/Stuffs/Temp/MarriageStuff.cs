using UnityEngine;

public class MarriageStuff : ClickableStuff
{
    [SerializeField] private GameObject document;



    protected override void Awake()
    {
        base.Awake();
        document.SetActive(false);
    }



    private void OnEnable()
    {
        inputComp.EnableInput();
        document.SetActive(false);
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        inputComp.DisableInput();
        document.SetActive(true);
    }
}
