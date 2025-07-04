using UnityEngine;

[CreateAssetMenu(menuName = "Game/TimeProgression")]
public class TimeProgressionData : ScriptableObject
{
    public ProgressTimeType type;
    public int minutes;
}