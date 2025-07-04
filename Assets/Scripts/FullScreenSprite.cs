using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FullscreenSprite : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // ���� ���� ȭ�� ũ�� ���
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Sprite ũ�� ��������
        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        // ũ�� ���� ���
        Vector3 scale = transform.localScale;
        scale.x = screenWidth / spriteWidth;
        scale.y = screenHeight / spriteHeight;
        transform.localScale = scale;
    }
}