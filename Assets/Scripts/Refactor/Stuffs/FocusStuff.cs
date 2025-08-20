using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class FocusStuff : ClickableStuff
{
    private static Dictionary<GameObject, GameObject> _instantiatedMap = new();
    [SerializeField] private GameObject focusPrefab;
    [SerializeField] private AudioClip outClip;
    private GameObject focusView;
    public GameObject FocusView => focusView;
    protected override StuffTypeData StuffData => GameData.FocusStuffData;



    protected override void Awake()
    {
        base.Awake();
        if (sfxClip == null)
            sfxClip = ControllerUtils.SFXClips.click1;
        if (outClip == null)
            outClip = sfxClip;
        focusView = Instantiate(focusPrefab);
        focusView.SetActive(false);

        // if (_instantiatedMap.ContainsKey(focusPrefab))
        // {
        //     focusView = _instantiatedMap[focusPrefab];
        // }
        // else
        // {
        //     _instantiatedMap[focusPrefab] = focusView;
        //     focusView = Instantiate(focusPrefab);
        //     focusView.SetActive(false);
        // }
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        GameManager.Instance.RoomController.FocusItem(focusView, outClip);
        TimeController.Instance.CheckTimeChanged();
    }
}
