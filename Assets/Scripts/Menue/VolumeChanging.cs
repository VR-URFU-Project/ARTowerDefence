using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeChanging : MonoBehaviour
{

    [SerializeField]
    AudioMixerGroup mixer;

    [SerializeField]
    Toggle musicToggle;

    [SerializeField]
    bool BackgroundMusic;

    private void Start()
    {
        //if (BackgroundMusic)
        //{
        //    var reader = QSReader.Create("BackMusicData");
        //    if (!reader.Exists("CurrentBackMusicVolume")) return;
        //    musicToggle.isOn = reader.Read<bool>("CurrentBackMusicVolume");
        //    if (reader.Read<bool>("CurrentBackMusicVolume"))
        //        backMusic.audioMixer.SetFloat("BackVolume", 0f);
        //    else backMusic.audioMixer.SetFloat("BackVolume", -80f);
        //}
        //else
        //{
        //    var reader = QSReader.Create("EffectsMusicData");
        //    if (!reader.Exists("CurrentEffectsMusicVolume")) return;
        //    musicToggle.isOn = reader.Read<bool>("CurrentEffectsMusicVolume");
        //    if (reader.Read<bool>("CurrentEffectsMusicVolume"))
        //        effects.audioMixer.SetFloat("EffectsVolume", 0f);
        //    else effects.audioMixer.SetFloat("EffectsVolume", -80f);
        //}

        string s;
        if (BackgroundMusic) s = "BackVolume";
        else s = "EffectsVolume";

        var reader = QSReader.Create("VolumeData");
        if (!reader.Exists("Current" + s)) return;
        //musicToggle.isOn = reader.Read<bool>("CurrentBackMusicVolume");
        if (reader.Read<bool>("Current"+s))
        {
            mixer.audioMixer.SetFloat(s, 0f);
            musicToggle.isOn = true;
        }
        else
        {
            mixer.audioMixer.SetFloat(s, -80f);
            musicToggle.isOn = false;
        }
    }

    public void ChangeMusicVolume()
    {
        //if (musicToggle.isOn)
        //    //backMusic.audioMixer.SetFloat("BackVolume", 0f);
        //else
        //    //backMusic.audioMixer.SetFloat("BackVolume", -80f);

        //var writer = QuickSaveWriter.Create("BackMusicData");
        //writer.Write("CurrentBackMusicVolume", musicToggle.isOn);
        //writer.Commit();
    }

    public void ChangeEffectsVolume()
    {
        //if (musicToggle.isOn)
        //    //effects.audioMixer.SetFloat("EffectsVolume", 0f);
        //else
        //    //effects.audioMixer.SetFloat("EffectsVolume", -80f);

        //var writer = QuickSaveWriter.Create("EffectsMusicData");
        //writer.Write("CurrentEffectsMusicVolume", musicToggle.isOn);
        //writer.Commit();
    }

    public void ChangeVolume()
    {
        string s;
        if (BackgroundMusic) s = "BackVolume";
        else s = "EffectsVolume";

        if (musicToggle.isOn)
            mixer.audioMixer.SetFloat(s, 0f);
        else
            mixer.audioMixer.SetFloat(s, -80f);
        
        var writer = QuickSaveWriter.Create("VolumeData");
        writer.Write("Current"+s, musicToggle.isOn);
        writer.Commit();
    }

    //public static void Off_EffectsVolume()
    //{
    //    effects.audioMixer.SetFloat("EffectsVolume", 0f);
    //}

    //public static void On_EffectsVolume()
    //{
    //    effects.audioMixer.SetFloat("EffectsVolume", -80f);
    //}
}
