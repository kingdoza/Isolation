using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] private LightSwitch lightSwitch;



    public void EnableLightSwitch()
    {
        lightSwitch.CanInteract = true;
    }



    public void DisableLightSwitch() 
    {
        lightSwitch.CanInteract = false;
    }   
}
