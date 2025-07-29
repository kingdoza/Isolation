using UnityEngine;

[CreateAssetMenu(fileName = "Triggers", menuName = "Scriptable Objects/Triggers")]
public class Triggers : ScriptableObject
{
    [SerializeField] private DialogueItem[] items;
}
