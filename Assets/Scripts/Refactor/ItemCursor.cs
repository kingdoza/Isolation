using UnityEngine;
using UnityEngine.UI;

public class ItemCursor : SceneSingleton<ItemCursor>
{
    private Image imageComp;
    private RectTransform rectTransformComp;



    protected override void Awake()
    {
        base.Awake();
        imageComp = GetComponent<Image>();
        rectTransformComp = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }



    public void Enable(ItemData itemData)
    {
        imageComp.sprite = itemData.Icon;
        rectTransformComp.position = Input.mousePosition;
        gameObject.SetActive(true);
    }



    public void Disable()
    {
        gameObject.SetActive(false);
        EtcUtils.SetCursorTexture();
    }



    private void Update()
    {
        rectTransformComp.position = Input.mousePosition;
    }
}
