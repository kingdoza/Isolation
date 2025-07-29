using UnityEngine;

[CreateAssetMenu(fileName = "MotiveUIData", menuName = "Scriptable Objects/MotiveUIData")]
public class MotiveUIData : ScriptableObject
{
    public GameObject slotPrefab;
    public Sprite lineSprite;
    public float lineThickness;
    public float itemSlotSize;
    public float clueSlotSize;
    public float finalSlotSize;
    public Vector2 finalSlotPosition;
}
