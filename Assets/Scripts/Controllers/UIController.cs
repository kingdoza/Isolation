using System.Collections.Generic;
using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<MoveButton> moveButtons;



    public void EnableMoveButtons(params MoveDirection[] directions)
    {
        foreach (var moveButton in moveButtons)
        {
            bool shouldEnable = Array.Exists(directions, dir => dir == moveButton.moveDir);
            moveButton.button.SetActive(shouldEnable);
        }
    }



    [System.Serializable]
    class MoveButton
    {
        public MoveDirection moveDir;
        public GameObject button;
    }
}



public enum MoveDirection
{
    Left, Right, Down
}