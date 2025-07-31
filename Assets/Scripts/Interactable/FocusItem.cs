using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class FocusItem : Item
{
    [SerializeField] private GameObject focusViewPrefab;



    protected override void Awake()
    {
        base.Awake();
        if(gameObject.GetComponent<MouseHover>() == null)
        {
            gameObject.AddComponent<MouseHover>();
        }
    }



    public override void Interact()
    {
        GameManager.Instance.RoomController.FocusItem(focusViewPrefab);

        SoundController soundPlayer = GameManager.Instance.SoundController;
        PlaySFX(SFXClips.click);
    }
}
