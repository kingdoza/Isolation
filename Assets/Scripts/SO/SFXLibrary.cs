using UnityEngine;

[CreateAssetMenu(fileName = "SFXLibrary", menuName = "Audio/SFXLibrary")]
public class SFXLibrary : ScriptableObject
{
    [SerializeField] private AudioClip click; public AudioClip Click => click;
    [SerializeField] private AudioClip lightSwitch; public AudioClip LightSwitch => lightSwitch;
}
