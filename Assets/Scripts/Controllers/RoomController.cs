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



    public void InitRoomAndShow()
    {
        uiController = GameManager.Instance.UIController;
        timeController = GameManager.Instance.TimeController;

        if (defaultRoom == null)
        {
            defaultRoom = rooms[0];
        }

        isZoomIn = false;
        MoveRoom(rooms[0]);
        ShowRoomView(defaultRoom.CurrentView);
    }



    public void ZoomOutView()
    {
        isZoomIn = false;
        ChangeRoomView(currentRoom.CurrentView);
        timeController.ProgressMinutes(ProgressTimeType.ZoomOut);
    }



    public void ZoomInView(GameObject newView) 
    {
        isZoomIn = true;
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
        Camera.main.gameObject.GetComponent<EgdeScroller>().InitPosAndSetView(currentView);
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
}