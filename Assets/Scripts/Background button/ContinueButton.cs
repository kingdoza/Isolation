using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public GameObject Background;

    public void ClickContinue()
    {
        Background.SetActive(false);
    }


    // Update is called once per frame
    
}
