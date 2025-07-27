using UnityEngine;

[CreateAssetMenu(fileName = "CursorLibrary", menuName = "Art/CursorLibrary")]
public class CursorLibrary : ScriptableObject
{
    [SerializeField] private Texture2D normal; public Texture2D Normal => normal;
    [SerializeField] private Texture2D click; public Texture2D Click => click;
}
