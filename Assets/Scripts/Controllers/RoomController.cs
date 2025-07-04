using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
    [SerializeField] private List<Room> rooms;
    private UIController uiController;
    private Room defaultRoom;
    private Room currentRoom;
    private GameObject currentView;
    private bool isZoomIn;



    public void InitRoomAndShow()
    {
        uiController = GameManager.Instance.UIController;

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
        ShowRoomView(currentRoom.CurrentView);
        SetViewMoveButtons();
    }



    public void ZoomInView(GameObject newView) 
    {
        isZoomIn = true;
        ShowRoomView(newView);
        SetViewMoveButtons();
    }



    private void ShowRoomView(GameObject newView)
    {
        if(currentRoom != null)
        {
            Destroy(currentView);
        }
        currentView = Instantiate(newView);
    }



    public void MoveLeft()
    {
        --currentRoom.ViewIndex;
        ShowRoomView(currentRoom.CurrentView);
    }



    public void MoveRight()
    {
        ++currentRoom.ViewIndex;
        ShowRoomView(currentRoom.CurrentView);
    }



    private void MoveRoom(Room newRoom, int viewIndex = 0)
    {
        isZoomIn = false;
        currentRoom = newRoom;
        currentRoom.ViewIndex = viewIndex;
        SetViewMoveButtons();
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