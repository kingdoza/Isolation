using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private Texture2D cursor;

    public ItemType Type => type;
    public Sprite Icon => icon;
    public Texture2D Cursor => cursor;
}



public enum ItemType
{
    None, ScrewDriver, Screw, DrawerHandle
}
