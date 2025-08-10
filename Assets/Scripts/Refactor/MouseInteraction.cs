using UnityEngine;
using UnityEngine.Events;

public abstract class MouseInteraction : MonoBehaviour
{
    protected abstract string InputLayerName { get; }
    public abstract bool IsPassDown { get; }



    protected virtual void Awake()
    {
        SetLayer();
    }



    private void SetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(InputLayerName);
    }



    public virtual void EnableInput()
    {
        SetLayer();
    }



    public virtual void DisableInput()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }



    public virtual void SetStatus(bool newStatus)
    {
        if (newStatus)
        {
            EnableInput();
        }
        else
        {
            DisableInput();
        }
    }



    public abstract void OnInteractStart();
    public abstract void OnInteracting();
    public abstract void OnInteractEnd();
    public abstract bool HasInteracted();



    public static void EnableSubObjectInputs(GameObject targetObject)
    {
        //MouseInteraction[] interactions = targetObject.GetComponentsInChildren<MouseInteraction>();
        //foreach (MouseInteraction interaction in interactions)
        //{
        //    interaction.EnableInput();
        //}

        BaseStuff[] baseStuffs = targetObject.GetComponentsInChildren<BaseStuff>();
        foreach (BaseStuff stuff in baseStuffs)
        {
            stuff.IsCovered = false;
        }
        targetObject.GetComponentInChildren<CameraDragArea>()?.EnableInput();
    }



    public static void DisableSubObjectInputs(GameObject targetObject)
    {
        //MouseInteraction[] interactions = targetObject.GetComponentsInChildren<MouseInteraction>();
        //foreach (MouseInteraction interaction in interactions)
        //{
        //    interaction.DisableInput();
        //}

        BaseStuff[] baseStuffs = targetObject.GetComponentsInChildren<BaseStuff>();
        foreach (BaseStuff stuff in baseStuffs)
        {
            Debug.Log(stuff.gameObject);
            stuff.IsCovered = true;
        }
        targetObject.GetComponentInChildren<CameraDragArea>()?.DisableInput();
    }
}
