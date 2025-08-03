using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [HideInInspector] public UnityEvent<Collider2D> TriggerEnterEvent = new UnityEvent<Collider2D>();
    [HideInInspector] public UnityEvent<Collider2D> TriggerExitEvent = new UnityEvent<Collider2D>();



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag) == false)
            return;
        TriggerEnterEvent?.Invoke(collision);
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag) == false)
            return;
        TriggerExitEvent?.Invoke(collision);
    }
}
