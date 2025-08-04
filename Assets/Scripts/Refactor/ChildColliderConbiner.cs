using UnityEngine;

public class ChildColliderCombiner : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            Collider2D childCollider = child.GetComponent<Collider2D>();
            if (childCollider == null)
                continue;

            
        }
    }
}
