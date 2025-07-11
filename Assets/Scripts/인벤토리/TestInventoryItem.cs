using UnityEngine;

public class TestItem : MonoBehaviour
{
    public string testitemName;       // 아이템 이름
    public Sprite testitemIcon;       // 아이템 아이콘

    // 아이템 클릭 시 호출될 메서드
    public void UseItem()
    {
        Debug.Log(testitemName + " used!");
        Destroy(gameObject); // 아이템 사용 후 삭제 (또는 다른 로직)
    }
}
