using UnityEngine;

public class FramePlate : StatusSwitcher
{
    [SerializeField] private bool isClose;
    public bool IsClose => isClose;
}
