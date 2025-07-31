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
        Player player = GameManager.Instance.Player;
        if (player == null || player.UsingItemType == UsableItem.None)
            SetCursorTexture(CursorTextures.Normal);
    }
}
