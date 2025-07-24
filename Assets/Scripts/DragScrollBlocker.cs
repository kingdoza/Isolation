using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class DragScrollBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isMouseOver = false;


    private void Start()
    {
        RegisterDragScrollCondition(() => !isMouseOver);
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}
