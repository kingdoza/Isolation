using UnityEngine;

public class HighestSortLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] renderers;



    public int GetHightestSortOrder()
    {
        int hightestSortOrder = -1;
        foreach (var renderer in renderers)
        {
            hightestSortOrder = Mathf.Max(hightestSortOrder, renderer.sortingOrder);
        }
        return hightestSortOrder;
    }
}
