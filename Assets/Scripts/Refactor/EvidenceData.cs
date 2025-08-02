using UnityEngine;

[CreateAssetMenu(fileName = "InteractTypeData", menuName = "Scriptable Objects/InteractTypeData")]
public class InteractTypeData : ScriptableObject
{
    [SerializeField] private LayerMask layerName; public LayerMask LayerName => layerName;
    [SerializeField] private Texture2D cursorTexture; public Texture2D CursorTexture => cursorTexture;

}
