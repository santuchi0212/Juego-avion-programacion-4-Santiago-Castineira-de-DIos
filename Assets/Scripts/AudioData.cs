using UnityEngine;

[CreateAssetMenu(fileName = "NuevoAudioData", menuName = "Audio/AudioData")]
public class AudioData : ScriptableObject
{
    [Range(0f, 1f)]
    public float volumenGeneral = 0.5f;
}