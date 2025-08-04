using UnityEngine;

public class DragData : MonoBehaviour
{
    [SerializeField] private float sentivity;
    [SerializeField] private float smoothTime;

    public float Sensitivity => sentivity;
    public float SmoothTime => smoothTime;
}
