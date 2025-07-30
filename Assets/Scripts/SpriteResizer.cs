using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteResizer : MonoBehaviour
{
    private const float TargetWidth = 19;
    [SerializeField] private float scale = 1;



    private void OnValidate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr.sprite == null) return;

        float spriteWidth = sr.sprite.bounds.size.x;
        float scaleX = TargetWidth / spriteWidth;
        transform.localScale = new Vector3(scaleX * scale, scaleX * scale, 1f);
    }
}