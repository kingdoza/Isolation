using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverSystem : SceneSingleton<MouseHoverSystem>
{
    [SerializeField] private LayerMask[] hoverLayers;
    private CursorHover hoverTarget;
    public CursorHover HoverTarget
    {
        get => hoverTarget; set
        {
            if (hoverTarget != value)
            {
                hoverTarget?.OnCursorExit();
                value?.OnCursorEnter();
            }
            hoverTarget = value;
        }
    }



    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            HoverTarget = null;
            return;
        }

        HoverTarget = GetHighestHoveredObject();
    }



    private CursorHover GetHighestHoveredObject()
    {
        CursorHover hoveredObject = null;
        foreach (LayerMask layer in hoverLayers)
        {
            if (hoveredObject = GetLayerHoveredObject(layer))
                break;
        }
        return hoveredObject;
    }



    private CursorHover GetLayerHoveredObject(LayerMask targetLayer)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, targetLayer);
        if (hit.collider == null)
            return null;
        return hit.collider.GetComponent<CursorHover>();
    }
}
