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



    public abstract void OnInteractStart();
    public abstract void OnInteracting();
    public abstract void OnInteractEnd();
    public abstract bool HasInteracted();



    public static void EnableSubObjectInputs(GameObject targetObject)
    {
        MouseInteraction[] interactions = targetObject.GetComponentsInChildren<MouseInteraction>();
        foreach (MouseInteraction interaction in interactions)
        {
            interaction.EnableInput();
        }
    }



    public static void DisableSubObjectInputs(GameObject targetObject)
    {
        MouseInteraction[] interactions = targetObject.GetComponentsInChildren<MouseInteraction>();
        foreach (MouseInteraction interaction in interactions)
        {
            interaction.DisableInput();
        }
    }
}
