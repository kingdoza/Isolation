using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class FocusStuff : ClickableStuff
{
    private static Dictionary<GameObject, GameObject> _instantiatedMap = new();
    [SerializeField] private GameObject focusPrefab;
    private GameObject focusView;
    public GameObject FocusView => focusView;
    protected override StuffTypeData StuffData => GameData.FocusStuffData;



    protected override void Awake()
    {
        base.Awake();
        //focusView = Instantiate(focusPrefab);
        //focusView.SetActive(false);

        if (_instantiatedMap.ContainsKey(focusPrefab))
        {
            focusView = _instantiatedMap[focusPrefab];
        }
        else
        {
            _instantiatedMap[focusPrefab] = focusView;
            focusView = Instantiate(focusPrefab);
            focusView.SetActive(false);
        }
    }



    protected override void OnClicked()
    {
        base.OnClicked();
        GameManager.Instance.RoomController.FocusItem(focusView);
    }
}
