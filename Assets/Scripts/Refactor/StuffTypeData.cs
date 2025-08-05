using UnityEngine;

[CreateAssetMenu(fileName = "StuffTypeData", menuName = "Scriptable Objects/StuffTypeData")]
public class StuffTypeData : ScriptableObject
{
    [SerializeField] private Texture2D cursorTexture; public Texture2D CursorTexture => cursorTexture;
    [SerializeField] private int minuteWaste; public int MinuteWaste => minuteWaste;
}
