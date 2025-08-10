using UnityEngine;

public class GenerateStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.ItemStuffData;
    [SerializeField] private GameObject stuffPrefab;



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        Instantiate(stuffPrefab);
        Destroy(gameObject);
    }
}
