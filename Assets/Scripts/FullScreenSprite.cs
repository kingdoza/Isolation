using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FullscreenSprite : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // 월드 기준 화면 크기 계산
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Sprite 크기 가져오기
        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        // 크기 비율 계산
        Vector3 scale = transform.localScale;
        scale.x = screenWidth / spriteWidth;
        scale.y = screenHeight / spriteHeight;
        transform.localScale = scale;
    }
}