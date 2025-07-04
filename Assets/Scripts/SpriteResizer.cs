using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteResizer : MonoBehaviour
{
    private const float TargetWidth = 1920;



    private void OnValidate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr.sprite == null) return;

        float spriteWidth = sr.sprite.rect.width;
        float scaleX = TargetWidth / spriteWidth;
        transform.localScale = new Vector3(scaleX, scaleX, 1f);
    }
}