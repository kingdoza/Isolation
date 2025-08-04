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



    private void Update()
    {
        if(uiController && !uiController.IsFading)
            HandleArrowKeyInput();
    }



    private void HandleArrowKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) && !isZoomIn)
        {
            PlaySFX(SFXClips.click);
            MoveLeft();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !isZoomIn)
        {
            PlaySFX(SFXClips.click);
            MoveRight();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && isZoomIn)
        {
            PlaySFX(SFXClips.click);
            ZoomOutView();
        }
    }



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



    public void FocusItem(GameObject focusView)
    {
        isFocusIn = true;
        if (itemFocusedView)
        {
            OutFocusItem();
        }
        MouseInteraction.DisableSubObjectInputs(currentView);
        focusPanel.SetActive(true);
        itemFocusedView = focusView;
        itemFocusedView.SetActive(true);
        uiController.EnableMoveButtons();
    }



    private void OutFocusItem()
    {
        isFocusIn = false;
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
        isZoomIn = false;
        //DragScroller.CanDrag = true;
        ChangeRoomView(currentRoom.CurrentView);
        timeController.ProgressMinutes(ProgressTimeType.ZoomOut);
    }



    public void ZoomInView(GameObject newView) 
    {
        isZoomIn = true;
        //DragScroller.CanDrag = false;
        ChangeRoomView(newView);
        timeController.ProgressMinutes(ProgressTimeType.ZoomIn);
    }



    private void ShowRoomView(GameObject newView)
    {
        if(currentView != null)
        {
            currentView.SetActive(false);
        }
        //currentView = Instantiate(newView);
        //GameManager.Instance.InteractController.SetTriggerItems(currentView);

        currentView = newView;
        currentView.SetActive(true);
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);

        //Camera.main.gameObject.GetComponent<DragScroller>().InitPosAndSetView(currentView);
        uiController.EnableMoveButtons();
    }



    private void ChangeRoomView(GameObject newView)
    {
        uiController.FadeOutThenIn(ShowRoomView, newView);
    }



    public void MoveLeft()
    {
        --currentRoom.ViewIndex;
        ChangeRoomView(currentRoom.CurrentView);
        timeController.ProgressMinutes(ProgressTimeType.Move);
    }



    public void MoveRight()
    {
        ++currentRoom.ViewIndex;
        ChangeRoomView(currentRoom.CurrentView);
        timeController.ProgressMinutes(ProgressTimeType.Move);
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