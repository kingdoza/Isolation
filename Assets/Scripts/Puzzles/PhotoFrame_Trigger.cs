using System;
using UnityEngine;

public class PhotoFrame_Trigger : MonoBehaviour
{
    private TriggerItem targetTrigger;
    [SerializeField] private SpriteRenderer targetRenderer;
    [SerializeField] private TriggerEntry oldFamilyPhoto;
    [SerializeField] private TriggerEntry graduationPhoto;
    private InteractSelector interactSelector;



    private void Awake()
    {
        oldFamilyPhoto.item.enabled = false;
        graduationPhoto.item.enabled = false;
        interactSelector = GetComponent<InteractSelector>();
    }



    private void Start()
    {
        GameManager.Instance.RoomController.OnFocusOut.AddListener(OnPhotoFrameFocusOut);
    }



    private void OnEnable()
    {
        SelectTrigger();
    }



    private void OnPhotoFrameFocusOut()
    {
        SelectTrigger();
    }



    private void SelectTrigger()
    {
        bool isPhotoConverted = GameManager.Instance.PuzzleController.PhotoFrameInfo.isConverted;
        TriggerEntry targetEntry = isPhotoConverted ? oldFamilyPhoto : graduationPhoto;
        //targetTrigger = targetEntry.item;
        targetRenderer.sprite = targetEntry.sprite;
        targetEntry.item.enabled = true;
        interactSelector.WakeupInteract = targetEntry.item;
    }



    [Serializable]
    public class TriggerEntry
    {
        public TriggerItem item;
        public Sprite sprite;
    }
}