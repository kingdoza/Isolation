using UnityEngine;

public class PersistentSingleton<T> : SceneSingleton<T> where T : MonoBehaviour {
    protected override void Awake() {
        base.Awake();
        if(transform.root != null)
            DontDestroyOnLoad(transform.root.gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}