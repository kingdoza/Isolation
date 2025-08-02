using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private void OnClick(InputValue value)
    {
        Debug.Log("OnClick" + value.Get<float>().ToString());
    }



    private void OnDrag(InputValue value)
    {
        Debug.Log("OnDrag" + value.Get<Vector2>().ToString());
    }

}
