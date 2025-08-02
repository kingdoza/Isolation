using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputSystem : SceneSingleton<MouseInputSystem>
{
    [SerializeField] private LayerMask[] inputLayers;
    private MouseInteraction inputTarget;



    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            inputTarget = null;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            inputTarget = GetHighestInteraction();
            inputTarget?.OnInteractStart();
        }

        if (Input.GetMouseButton(0))
        {
            inputTarget?.OnInteracting();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (inputTarget == null) return;

            if (inputTarget.HasInteracted())
            {
                inputTarget.OnInteractEnd();
            }
            else if (inputTarget.IsPassDown)
            {
                inputTarget = GetHighestInteractionBelowLayer(inputTarget.gameObject);
                inputTarget?.OnInteractEnd();
            }
            inputTarget = null;
        }
    }



    private MouseInteraction GetHighestInteraction()
    {
        MouseInteraction interaction = null;
        foreach (LayerMask layer in inputLayers)
        {
            if (interaction = GetLayerInteraction(layer))
                break;
        }
        return interaction;
    }



    private MouseInteraction GetHighestInteractionBelowLayer(GameObject targetObject)
    {
        int targetLayer = targetObject.layer;
        int targetMask = 1 << targetLayer;
        int index = Array.FindIndex(inputLayers, mask => (mask.value & targetMask) != 0);

        MouseInteraction interaction = null;
        for (int i = index + 1; i < inputLayers.Length; ++i)
        {
            if (interaction = GetLayerInteraction(inputLayers[i]))
                break;
        }
        return interaction;
    }



    private MouseInteraction GetLayerInteraction(LayerMask targetLayer)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, targetLayer);
        if (hit.collider == null)
            return null;
        return hit.collider.GetComponent<MouseInteraction>();
    }
}
