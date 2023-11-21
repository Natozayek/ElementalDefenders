using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to load the audio settings into other scenes, such as Gameplay scene or Game Over scene.

public class AudioSettings : MonoBehaviour
{
    private static string MusicPref = "MusicPref";
    private static string SfxPref = "SfxPref";

    private float musicVolume, sfxVolume;

    public AudioSource backgroundMusicAudio;
    public AudioSource[] sfxAudio;

    // Start is called before the first frame update
    void Start()
    {
        maintainSettings();
    }

    private void maintainSettings()
    {
        musicVolume = PlayerPrefs.GetFloat(MusicPref);
        sfxVolume = PlayerPrefs.GetFloat(SfxPref);

        backgroundMusicAudio.volume = musicVolume;

        for(int i = 0; i < sfxAudio.Length; i++)
        {
            sfxAudio[i].volume = sfxVolume;
        }
    }
}
