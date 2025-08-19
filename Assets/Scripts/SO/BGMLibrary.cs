using UnityEngine;

[CreateAssetMenu(fileName = "BGMLibrary", menuName = "Audio/BGMLibrary")]
public class BGMLibrary : ScriptableObject
{
    public AudioClip main;
    public AudioClip inGame;
    public AudioClip timeoutEnding;
    public AudioClip trueEnding;
    public AudioClip badEnding;
}
