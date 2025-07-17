using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
    [SerializeField] private List<Room> rooms;
    private UIController uiController;
    private TimeController timeController;
    private Room defaultRoom;
    private Room currentRoom;
    private GameObject currentView;
    private bool isZoomIn;

    public GameObject CurrentView => currentView;



    private void Update()
    {
        if(uiController && !uiController.IsFading)
            HandleArrowKeyInput();
    }



    private void HandleArrowKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) && !isZoomIn)
        {
            MoveLeft();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !isZoomIn)
        {
            MoveRight();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && isZoomIn)
        {
            ZoomOutView();
        }
    }



    public void InitRoomAndShow()
    {
        uiController = GameManager.Instance.UIController;
        timeController = GameManager.Instance.TimeController;

        if (defaultRoom == null)
        {
            defaultRoom = rooms[0];
        }

        isZoomIn = false;
        DragScroller.CanDrag = true;
        MoveRoom(rooms[0]);
        ShowRoomView(defaultRoom.CurrentView);
    }



    public void ZoomOutView()
    {
        isZoomIn = false;
        DragScroller.CanDrag = true;
        ChangeRoomView(currentRoom.CurrentView);
        timeController.ProgressMinutes(ProgressTimeType.ZoomOut);
    }



    public void ZoomInView(GameObject newView) 
    {
        isZoomIn = true;
        DragScroller.CanDrag = false;
        ChangeRoomView(newView);
        timeController.ProgressMinutes(ProgressTimeType.ZoomIn);
    }



    private void ShowRoomView(GameObject newView)
    {
        if(currentRoom != null)
        {
            Destroy(currentView);
        }
        currentView = Instantiate(newView);
        Camera.main.gameObject.GetComponent<DragScroller>().InitPosAndSetView(currentView);
        SetViewMoveButtons();
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
            uiController.EnableMoveButtons(MoveDirection.Down);
            return;
        }

        if (currentRoom.Views.Count > 1)
        {
            uiController.EnableMoveButtons(MoveDirection.Left, MoveDirection.Right);
        }
        else
        {
            uiController.EnableMoveButtons();
        }
    }



    public void DestroyView()
    {
        Destroy(currentView);
    }
}