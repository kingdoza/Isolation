using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room : MonoBehaviour
{
    [SerializeField] private string roomName;
    [SerializeField] private List<GameObject> views;
    private int viewIndex;



    public List<GameObject> Views => views;
    public int ViewIndex
    {
        get => viewIndex;
        set => viewIndex = (value % views.Count + views.Count) % views.Count;
    }
    public GameObject CurrentView => views[viewIndex];
}