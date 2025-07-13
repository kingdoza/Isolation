using UnityEngine;
using UnityEngine.Rendering;

public class FilterController : MonoBehaviour
{
    [SerializeField] private Volume filter;



    public void SetWakeup()
    {
        filter.enabled = false;
    }



    public void SetSleep()
    {
        filter.enabled = true;
    }
}
