using UnityEngine;

public class Screw : StatusSwitcher
{
    [SerializeField] private bool isJoint;
    public bool IsJoint => isJoint;
}
