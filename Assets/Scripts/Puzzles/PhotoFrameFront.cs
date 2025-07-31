using UnityEngine;

public class PhotoFrameFront : MonoBehaviour
{
    [SerializeField] private SpriteRenderer photoRenderer;
    [SerializeField] private Sprite graduationPhoto;
    [SerializeField] private Sprite oldFamiliyPhoto;



    private void Start()
    {
        PhotoFrame.Info info = GameManager.Instance.PuzzleController.PhotoFrameInfo;
        photoRenderer.sprite = info.isConverted ? oldFamiliyPhoto : graduationPhoto;
    }
}
