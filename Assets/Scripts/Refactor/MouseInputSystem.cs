using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputSystem : SceneSingleton<MouseInputSystem>
{
    private readonly static List<string> _sortingLayerNames = new List<string>();
    [SerializeField] private LayerMask[] inputLayers;
    private MouseInteraction inputTarget;

    private string allowedSortingLayer = SortingLayerAll;
    private const string SortingLayerDisabled = "__none__";
    private const string SortingLayerAll = "__all__";



    protected override void Awake()
    {
        base.Awake();

        for (int i = SortingLayer.layers.Length - 1; i >= 0; --i)
        {
            _sortingLayerNames.Add(SortingLayer.layers[i].name);
        }
    }



    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())// ||
            //(inputTarget && IsAllowedInputObject(inputTarget.gameObject)))
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



    public bool IsAllowedInputObject(GameObject targetObject)
    {
        if (!targetObject) 
            return false;
        if (allowedSortingLayer.Equals(SortingLayerAll))
            return true;
        string targetLayerName = targetObject.GetComponent<SpriteRenderer>().sortingLayerName;
        return targetLayerName.Equals(allowedSortingLayer);
    }


    
    private MouseInteraction GetHighestInteraction()
    {
        MouseInteraction interaction = null;
        foreach (string sortLayer in _sortingLayerNames)
        {
            foreach (LayerMask layer in inputLayers)
            {
                if (interaction = GetLayerInteraction(layer))
                    break;
            }
        }
        return interaction;
    }



    private MouseInteraction GetHighestInteractionBelowLayer(GameObject targetObject)
    {
        int targetLayer = targetObject.layer;
        int targetMask = 1 << targetLayer;
        int index = Array.FindIndex(inputLayers, mask => (mask.value & targetMask) != 0);

        MouseInteraction interaction = null;
        foreach (string sortLayer in _sortingLayerNames)
        {
            for (int i = index + 1; i < inputLayers.Length; ++i)
            {
                if (interaction = GetLayerInteraction(inputLayers[i]))
                    break;
            }
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
