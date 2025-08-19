using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] infoPanels;
    private int currentIdx = 0;



    private void Awake()
    {
        if (GameManager.Instance.isTutorial == false)
        {
            gameObject.SetActive(false);
            return;
        }

        foreach (GameObject panel in infoPanels)
        {
            panel.SetActive(false);
        }
        infoPanels[0].SetActive(true);
    }



    public void SkipNextPanel()
    {
        infoPanels[currentIdx++].SetActive(false);
        if (currentIdx >= infoPanels.Length)
        {
            gameObject.SetActive(false);
            GameManager.Instance.isTutorial = false;
            return;
        }
        infoPanels[currentIdx].SetActive(true);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SkipNextPanel();
        }
    }
}
