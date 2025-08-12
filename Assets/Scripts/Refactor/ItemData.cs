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



    /// <summary>
    /// 커서 텍스처를 배율만큼 확대한 복사본 반환
    /// </summary>
    public Texture2D GetScaledCursor()
    {
        if (cursor == null) return null;

        Rect spriteRect = icon.rect;
        Debug.Log((int)spriteRect.width);
        Debug.Log((int)spriteRect.height);
        Texture2D spriteTexture = icon.texture;
        int max = (int)Mathf.Max(spriteRect.width, spriteRect.height);
        int min = (int)Mathf.Min(spriteRect.width, spriteRect.height);
        Texture2D cursorTexture = new Texture2D(max, max);
        Color[] pixels = spriteTexture.GetPixels(
            (int)spriteRect.x,
            (int)spriteRect.y,
            max,
            max
        );
        cursorTexture.SetPixels(pixels);
        cursorTexture.Apply();
        Debug.Log((int)cursorTexture.width);
        Debug.Log((int)cursorTexture.height);
        return cursorTexture;
    }



    /// <summary>
    /// 커서 텍스처 색상 변경 후 복사본 반환
    /// </summary>
    public Texture2D GetTintedCursor(Color tint)
    {
        if (cursor == null) return null;

        Texture2D tinted = new Texture2D(cursor.width, cursor.height, cursor.format, false);
        Color[] pixels = cursor.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            float alpha = pixels[i].a;
            pixels[i] = tint * pixels[i];
            pixels[i].a = alpha;
        }

        tinted.SetPixels(pixels);
        tinted.Apply();
        return tinted;
    }
}



public enum ItemType
{
    None, ScrewDriver, Screw, DrawerHandle
}
