using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using UnityEngine.Audio;

public class OnMenuStart : MonoBehaviour
{
    [SerializeField]
    AudioMixerGroup backMusic;

    [SerializeField]
    AudioMixerGroup effects;

    void Start()
    {
        var reader = QSReader.Create("VolumeData");
        if (reader.Exists("CurrentBackVolume") && reader.Read<bool>("CurrentBackVolume"))
            backMusic.audioMixer.SetFloat("BackVolume", 0f);
        else
            backMusic.audioMixer.SetFloat("BackVolume", -80f);

        reader = QSReader.Create("VolumeData");
        if (reader.Exists("CurrentEffectsVolume") && reader.Read<bool>("CurrentEffectsVolume"))
            effects.audioMixer.SetFloat("EffectsVolume", 0f);
        else
            effects.audioMixer.SetFloat("EffectsVolume", -80f);
    }

}
