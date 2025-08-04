using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room : MonoBehaviour
{
    [SerializeField] private string roomName;
    [SerializeField] private List<GameObject> viewPrefabs;
    private List<GameObject> views = new List<GameObject>();
    private int viewIndex;



    public void CreateViewInstances()
    {
        foreach (GameObject viewPrefab in viewPrefabs)
        {
            GameObject view = Instantiate(viewPrefab);
            view.SetActive(false);
            views.Add(view);
        }
    }



    public List<GameObject> Views => views;
    public int ViewIndex
    {
        get => viewIndex;
        set => viewIndex = (value % views.Count + views.Count) % views.Count;
    }
    public GameObject CurrentView => views[viewIndex];
}