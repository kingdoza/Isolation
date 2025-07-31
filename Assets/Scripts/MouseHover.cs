using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Player player = GameManager.Instance.Player;
        if (player == null || player.UsingItemType == UsableItem.None)
            SetCursorTexture(CursorTextures.Click);
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        Player player = GameManager.Instance.Player;
        if (player == null || player.UsingItemType == UsableItem.None)
            SetCursorTexture(CursorTextures.Normal);
    }



    private void OnDisable()
    {
        if (DraggableItem.isDragging)
            return;
        Player player = GameManager.Instance.Player;
        if (player == null || player.UsingItemType == UsableItem.None)
        {
            SetCursorTexture(CursorTextures.Normal);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
