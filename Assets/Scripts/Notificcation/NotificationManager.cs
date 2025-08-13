using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    [Header("NotificationUI")]
    public GameObject MindMove;
    
    public TextMeshProUGUI countText;

    private int notificationCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        notificationCount = 0;
        UpdateUI(); 
    }

    public void ShowNotification()
    {
        notificationCount++;
        UpdateUI();
    }

    public void CheckNotifications()
    {
        if (notificationCount == 0) return;
        notificationCount = 0;
        UpdateUI();
    }

    
    private void UpdateUI()
    {
        if (countText == null) return;

        if (notificationCount > 0)
        {
           
            countText.text = notificationCount.ToString();
        }
        else
        {
            
            countText.text = ""; 
        }
    }
}