using UnityEngine;

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


    public abstract void OnInteractStart();
    public abstract void OnInteracting();
    public abstract void OnInteractEnd();
    public abstract bool HasInteracted();
}
