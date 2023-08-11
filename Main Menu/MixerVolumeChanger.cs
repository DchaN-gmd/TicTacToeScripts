using UnityEngine;
using UnityEngine.Audio;

public class MixerVolumeChanger : MonoBehaviour
{
    [SerializeField] private string _settingsName;
    [SerializeField] private AudioMixerGroup _mixer;

    private void Awake()
    {
        
    }

    public void ChangeVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(_settingsName, Mathf.Lerp(-80, 0, volume));
    }
}
