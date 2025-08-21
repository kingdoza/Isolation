using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static ControllerUtils;

public class RoomController : MonoBehaviour {
    public HashSet<string> CollectedItemNames { get; set; } = new HashSet<string>();

    [SerializeField] private Color focusBackgroundColor;
    [SerializeField] private GameObject focusPanel;
    [SerializeField] private List<Room> rooms;
    private UIController uiController;
    private TimeController timeController;
    private Room defaultRoom;
    private Room currentRoom;
    private GameObject currentView;
    private bool isZoomIn;
    public bool IsZoomIn => isZoomIn;

    private bool isFocusIn;
    public bool IsFocusIn => isFocusIn;
    private GameObject itemFocusedView;

    public GameObject CurrentView => currentView;

    public UnityEvent OnFocusOut = new UnityEvent();
    // Stack<GameObject> zoomStack = new Stack<GameObject>();
    private Stack<GameObject> zoomStack = new Stack<GameObject>();
    private AudioClip focusInClip;
    private Stack<AudioClip> zoomClipStack = new Stack<AudioClip>();



    private void Awake()
    {
    }



    private void Update()
    {
        //if(uiController && !uiController.IsFading)
        //    HandleArrowKeyInput();
    }



    private void OnViewFadeCompleted()
    {
        
    }



    //private void HandleArrowKeyInput()
    //{
    //    if (Input.GetKeyUp(KeyCode.LeftArrow) && !isZoomIn)
    //    {
    //        PlaySFX(SFXClips.click);
    //        MoveLeft();
    //    }
    //    if (Input.GetKeyUp(KeyCode.RightArrow) && !isZoomIn)
    //    {
    //        PlaySFX(SFXClips.click);
    //        MoveRight();
    //    }
    //    if (Input.GetKeyUp(KeyCode.DownArrow) && isZoomIn)
    //    {
    //        PlaySFX(SFXClips.click);
    //        ZoomOutView();
    //    }
    //}



    public void InitRoomAndShow()
    {
        foreach (Room room in rooms)
        {
            room.CreateViewInstances();
        }

        uiController = GameManager.Instance.UIController;
        timeController = GameManager.Instance.TimeController;

        if (defaultRoom == null)
        {
            defaultRoom = rooms[0];
        }

        isZoomIn = false;
        //DragScroller.CanDrag = true;
        MoveRoom(rooms[0]);
        ShowRoomView(defaultRoom.CurrentView);
    }



    public void ReturnToRoomView()
    {
        if (isZoomIn == false && isFocusIn == false) return;

        if (isFocusIn)
        {
            OutFocusItem();
        }
        isZoomIn = false;
        zoomStack.Clear();
        ChangeRoomView(currentRoom.CurrentView);
    }



    public void DisableRoomViews()
    {
        foreach (GameObject roomView in currentRoom.Views)
        {
            BaseStuff[] roomStuffs = roomView.GetComponentsInChildren<BaseStuff>();
            foreach (BaseStuff stuff in roomStuffs)
            {
                if (stuff is DoorStuff)
                    (stuff as DoorStuff).canOpen = true;
                    //stuff.GetComponent<MouseInteraction>().EnableInput();
                else
                    Destroy(stuff);
                    //stuff.GetComponent<MouseInteraction>().DisableInput();
            }
        }
    }



    public void FocusItem(GameObject focusView, AudioClip focusClip)
    {
        isFocusIn = true;
        focusInClip = focusClip; //
        if (itemFocusedView)
        {
            OutFocusItem();
        }
        MouseInteraction.DisableSubObjectInputs(currentView);
        focusPanel.SetActive(true);
        itemFocusedView = focusView;
        Vector3 cameraPos = Camera.main.transform.position;
        itemFocusedView.transform.position = new Vector3(cameraPos.x, cameraPos.y, 0);
        itemFocusedView.SetActive(true);
        if (itemFocusedView.name.Equals("Focus_Computer(Clone)"))
        {
            GameManager.Instance.FilterController.SetMonitorNightFilter();
        }
        uiController.EnableMoveButtons();
    }



    private void OutFocusItem()
    {
        isFocusIn = false;
        PlaySFX(focusInClip);
        if (itemFocusedView.name.Equals("Focus_Computer(Clone)"))
        {
            GameManager.Instance.FilterController.SetMotiveFilter(null, string.Empty);
            if (Player.Instance.IsSleeping)
                GameManager.Instance.FilterController.SetSleep();
            else
                GameManager.Instance.FilterController.SetWakeup();
        }
        itemFocusedView.SetActive(false);
        MouseInteraction.EnableSubObjectInputs(currentView);
        itemFocusedView = null;
        //Destroy(itemFocusedView);
        focusPanel.SetActive(false);
        uiController.EnableMoveButtons();
        OnFocusOut?.Invoke();
        //SetOtherRoomObjectsColor(null, Color.white);
        //SetViewbjectsColorAndStatus(Color.white, true);
    }



    public void ZoomOutView()
    {
        if (itemFocusedView) 
        {
            OutFocusItem();
            return;
        }
        PlaySFX(zoomClipStack.Pop());
        if (zoomStack.Count != 0)
        {
            isZoomIn = true;
            ChangeRoomView(zoomStack.Pop());
        }
        else
        {
            isZoomIn = false;
            ChangeRoomView(currentRoom.CurrentView);

        }
        //isZoomIn = false;
        //ChangeRoomView(zoomStack.Pop());
    }



    public void ZoomInView(GameObject newView, AudioClip zoomClip)
    {
        if (isZoomIn)
        {
            zoomStack.Push(currentView);
        }
        zoomClipStack.Push(zoomClip);
        isZoomIn = true;
        //DragScroller.CanDrag = false;
        ChangeRoomView(newView);
        //timeController.ProgressMinutes(ProgressTimeType.ZoomIn);
    }



    private void ShowRoomView(GameObject newView)
    {
        if(currentView != null)
        {
            currentView.SetActive(false);
        }
        //currentView = Instantiate(newView);
        //GameManager.Instance.InteractController.SetTriggerItems(currentView);
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);

        currentView = newView;
        currentView.SetActive(true);
        //Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);

        //Camera.main.gameObject.GetComponent<DragScroller>().InitPosAndSetView(currentView);
        uiController.EnableMoveButtons();
    }



    private void ChangeRoomView(GameObject newView)
    {
        uiController.FadeOutThenIn(ShowRoomView, newView);
    }



    public void MoveLeft()
    {
        PlaySFX(SFXClips.tutorial);
        --currentRoom.ViewIndex;
        ChangeRoomView(currentRoom.CurrentView);
        //timeController.ProgressMinutes(ProgressTimeType.Move);
        if (GameManager.Instance.EndingType == EndingType.None)
            timeController.ProgressMinutes(GameData.MoveSidewayMinutes);
    }



    public void MoveRight()
    {
        PlaySFX(SFXClips.tutorial);
        ++currentRoom.ViewIndex;
        ChangeRoomView(currentRoom.CurrentView);
        //timeController.ProgressMinutes(ProgressTimeType.Move);
        if(GameManager.Instance.EndingType == EndingType.None)
            timeController.ProgressMinutes(GameData.MoveSidewayMinutes);
    }



    private void MoveRoom(Room newRoom, int viewIndex = 0)
    {
        isZoomIn = false;
        currentRoom = newRoom;
        currentRoom.ViewIndex = viewIndex;
    }



    private void SetViewMoveButtons()
    {
        if (isZoomIn)
        {
            uiController.EnableMoveButtons();
            //uiController.EnableMoveButtons(MoveDirection.Down);
            return;
        }

        if (currentRoom.Views.Count > 1)
        {
            uiController.EnableMoveButtons();
            //uiController.EnableMoveButtons(MoveDirection.Left, MoveDirection.Right);
        }
        else
        {
            //uiController.EnableMoveButtons();
            uiController.DisableMoveButtons();
        }
    }



    private void SetViewbjectsColorAndStatus(Color spriteColor, bool interactStatus)
    {
        GameObject[] viewObjects = GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToArray();
        foreach (GameObject viewObj in viewObjects)
        {
            SpriteRenderer sr = viewObj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = spriteColor;
            }
            IInteractable interactable = viewObj.GetComponent<IInteractable>();
            if (interactable != null)
            {
                //interactable.CanInteract = interactStatus;
            }
        }
    }



    public void SetOtherRoomObjectsColor(GameObject exceptObject, Color color)
    {
        SpriteRenderer[] childsRenderers = CurrentView.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in childsRenderers)
        {
            if (exceptObject != null && exceptObject.GetComponent<SpriteRenderer>() == renderer)
                continue;
            renderer.color = color;
        }
    }



    public void DestroyView()
    {
        Destroy(currentView);
    }
}