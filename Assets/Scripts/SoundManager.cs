using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static string MASTER_VOLUME_PARAMETER = "Master";
    private static string SFX_VOLUME_PARAMETER = "SFX";
    private static string MUSIC_VOLUME_PARAMETER = "Music";

    [SerializeField] private AudioMixerGroup mainMixer;
    [SerializeField] private AudioMixerGroup SFXMixer;
    [SerializeField] private AudioMixerGroup musicMixer;

    public void MasterVolumeSliderChanged(float newValue)
    {
        float actualVolumeValue = (newValue);
        mainMixer.audioMixer.SetFloat(MASTER_VOLUME_PARAMETER, newValue);
    }

    public void SFXVolumeSliderChanged(float newValue)
    {
        float actualVolumeValue = (newValue);
        SFXMixer.audioMixer.SetFloat(SFX_VOLUME_PARAMETER, newValue);
    }

    public void MusicVolumeSliderChanged(float newValue)
    {
        float actualVolumeValue = (newValue);
        musicMixer.audioMixer.SetFloat(MUSIC_VOLUME_PARAMETER, newValue);
    }

}
