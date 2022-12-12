using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChanging : MonoBehaviour
{
    [SerializeField]
    AudioMixerGroup backMusic;
    
    [SerializeField]
    AudioMixerGroup effects;
    
    [SerializeField]
    Slider musicSlider;

    [SerializeField]
    bool BackgroundMusic;

    private void Start()
    {

        if (BackgroundMusic)
        {
            var reader = QSReader.Create("BackMusicData");
            if (!reader.Exists("CurrentBackMusicVolume")) return;
            musicSlider.value = reader.Read<float>("CurrentBackMusicVolume");
        }
        else
        {
            var reader = QSReader.Create("EffectsMusicData");
            if (!reader.Exists("CurrentEffectsMusicVolume")) return;
            musicSlider.value = reader.Read<float>("CurrentEffectsMusicVolume");
        }
    }

    public void ChangeMusicVolume()
    {
        backMusic.audioMixer.SetFloat("BackVolume", Mathf.Lerp(-80, 0, musicSlider.value));
        var writer = QuickSaveWriter.Create("BackMusicData");
        writer.Write("CurrentBackMusicVolume", musicSlider.value);
        writer.Commit();
    }

    public void ChangeEffectsVolume()
    { 
        effects.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, musicSlider.value));
        var writer = QuickSaveWriter.Create("EffectsMusicData");
        writer.Write("CurrentEffectsMusicVolume", musicSlider.value);
        writer.Commit();
    }
}
