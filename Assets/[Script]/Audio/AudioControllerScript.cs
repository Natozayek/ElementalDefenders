using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
//Script to set and save audio preferences in the main menu scene.

public class AudioControllerScript : MonoBehaviour
{
    private static UnityEvent<float, float> UpdateSoundSetting = new UnityEvent<float, float>();

    private static string sFirstPlay = "sFirstPlay";
    private static string MusicPref = "MusicPref";
    private static string SfxPref = "SfxPref";

    private int firstPlay;
    public Slider musicSlider, sfxSlider;
    private float musicVolume, sfxVolume;

    public AudioSource backgroundMusicAudio;
    public AudioSource[] sfxAudio;

    private bool hadLoaded = false;

    private void Awake()
    {
        UpdateSoundSetting.AddListener(this._UpdateSound);
    }

    private void OnDestroy()
    {
        UpdateSoundSetting.RemoveListener(this._UpdateSound);
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            UpdateSoundSetting.AddListener(this._UpdateSound);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        firstPlay = PlayerPrefs.GetInt(sFirstPlay);

        if(firstPlay == 0)
        {
            musicVolume = 1f;
            sfxVolume = 1f;
            musicSlider.value = musicVolume;
            sfxSlider.value = sfxVolume;
            PlayerPrefs.SetFloat(MusicPref, musicVolume);
            PlayerPrefs.SetFloat(SfxPref, sfxVolume);
            PlayerPrefs.SetInt(sFirstPlay, -1);
        }
        else
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();

            musicSlider.value = PlayerPrefs.GetFloat(MusicPref);
            sfxSlider.value = PlayerPrefs.GetFloat(SfxPref);

            musicSlider.onValueChanged.AddListener(UpdateSound);
            sfxSlider.onValueChanged.AddListener(UpdateSound);

            UpdateSound();
        }
        hadLoaded = true;
    }

    public void SaveSoundSettings()
    {
        //if (hadLoaded)
        //{
        //    PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
        //    PlayerPrefs.SetFloat(SfxPref, sfxSlider.value);
        //}
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound(float f = 0)
    {
        UpdateSoundSetting.Invoke(musicSlider.value, sfxSlider.value);
    }

    public void _UpdateSound(float music, float sfx)
    {
        backgroundMusicAudio.volume = music;

        for (int i = 0; i < sfxAudio.Length; i++)
        {
            sfxAudio[i].volume = sfx;
        }

        if (hadLoaded)
        {
            PlayerPrefs.SetFloat(MusicPref, music);
            PlayerPrefs.SetFloat(SfxPref, sfx);
        }

        //SaveSoundSettings();
    }
}
