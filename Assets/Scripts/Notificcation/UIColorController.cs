using UnityEngine;
using UnityEngine.UI;

public class UIColorController : MonoBehaviour
{
    public static UIColorController Instance;
    public GameObject MindUI;
    private Image UiImage;


    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        

        UiImage = MindUI.GetComponent<Image>();
    }

    public void ChangeImageColor()
    {
        if (UiImage != null)
        {
            UiImage.color = Color.red;
        }

    }

    public void ReturnColor()
    {
        if (UiImage != null)
        {
            UiImage.color = Color.white;
        }
    }
}