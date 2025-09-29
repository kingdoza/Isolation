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

    public Sprite happyEvidenceSprite;
    public Sprite happyMemorySprite;
    public Sprite happyEndingSprite;
    public Sprite badEvidenceSprite;
    public Sprite badMemorySprite;
    public Sprite badEndingSprite;
}
