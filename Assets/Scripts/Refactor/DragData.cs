using UnityEngine;
using UnityEngine.UI;

public class DragData : MonoBehaviour
{
    [SerializeField] private float sentivity;
    [SerializeField] private float smoothTime;
    [SerializeField] private LayoutElement rightLayout;

    public float Sensitivity => sentivity;
    public float SmoothTime => smoothTime;
    public LayoutElement RightLayout => rightLayout;
}
