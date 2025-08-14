using UnityEngine;

public class Diary : SingleConditonActivator
{
    [SerializeField] private GameObject diaryOpened;
    [SerializeField] private GameObject bookMark;
    [SerializeField] private GameObject keyLocker;



    private void OnEnable()
    {
        bookMark.SetActive(true);
        diaryOpened.SetActive(false);
    }



    protected override void SetTrueComponent()
    {
        keyLocker.SetActive(false);
    }



    protected override void SetFalseComponent()
    {
        keyLocker.SetActive(true);
    }
}
