using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetCursorTexture(CursorTextures.Click);
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        SetCursorTexture(CursorTextures.Normal);
    }



    private void OnDisable()
    {
        SetCursorTexture(CursorTextures.Normal);
    }
}
