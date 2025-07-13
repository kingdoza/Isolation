using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    bool CanInteract { get; set; }
    void Interact();
}
