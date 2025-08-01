using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color originalColor;



    private void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }


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
            GetComponent<SpriteRenderer>().color = originalColor;
        }
    }
}
