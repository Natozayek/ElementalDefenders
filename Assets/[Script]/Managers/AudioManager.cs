using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundLibrary libarary;

    [Range(0.0f, 1.0f)]
    public float Volume = 0.5f;


    private static Dictionary<string, SoundLibrary.Sound> SoundDictionary = new Dictionary<string, SoundLibrary.Sound>();
    private static Dictionary<string, SoundLibrary.Sound> MusicDictionary = new Dictionary<string, SoundLibrary.Sound>();
    private static AudioManager _Instance = null;
    private static int AudioChannel = 24;
    private static List<AudioSource> Channels = new List<AudioSource>();
    private static AudioSource MusicChannels = null;


    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);


            foreach(var sound in libarary.Sounds)
            {
                if(!SoundDictionary.ContainsKey(sound.ID))
                {
                    SoundDictionary.Add(sound.ID, sound);
                }
                else
                {
                    Debug.LogWarning(string.Format("{0} already exist", sound.ID));
                }
            }

            foreach (var sound in libarary.Music)
            {
                if (!MusicDictionary.ContainsKey(sound.ID))
                {
                    MusicDictionary.Add(sound.ID, sound);
                }
                else
                {
                    Debug.LogWarning(string.Format("{0} already exist", sound.ID));
                }
            }


            // Spawn a number of game object containing AudioSource component
            for (int i = 0; i < AudioChannel; i++)
            {
                var channel = new GameObject("Channel " + i.ToString());
                channel.transform.parent = this.transform;
                channel.AddComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SfxPref");
                Channels.Add(channel.GetComponent<AudioSource>());
            }

            var musicChannel = new GameObject("Music Channel");
            musicChannel.transform.parent = this.transform;
            musicChannel.AddComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicPref");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static AudioManager Instance
    {
        get
        {
            if (_Instance != null)
            {
                return _Instance;
            }
            return null;
        }
    }

    // Play the audio clip on an AudioSource that is not playing anything
    // Then return true
    // If there isn't any return false
    public bool PlayAudio(AudioClip Clip)
    {
        foreach(var channel in Channels)
        {
            if(!channel.isPlaying)
            {
                //channel.volume = Volume;
                channel.volume = PlayerPrefs.GetFloat("SfxPref");
                channel.clip = Clip;
                channel.Play();
                return true;
            }
        }
        return false;
    }

    public bool StopAudio(AudioClip Clip)
    {
        foreach (var channel in Channels)
        {
            if (channel.clip == Clip)
            {
                channel.Stop();
                return true;
            }
        }
        return false;
    }

    public bool PlayAudio(string Id)
    {
        if(SoundDictionary.ContainsKey(Id))
        {
            var tempRef = SoundDictionary[Id];
            return PlayAudio(tempRef.Audio);
        }
        return false;

    }

    public bool StopAudio(string Id)
    {
        if (SoundDictionary.ContainsKey(Id))
        {
            var tempRef = SoundDictionary[Id];
            return StopAudio(tempRef.Audio);
        }
        return false;
    }

    public bool PlayMusic(AudioClip Clip)
    {
        if (!MusicChannels.isPlaying)
        {
            //channel.volume = Volume;
            MusicChannels.volume = PlayerPrefs.GetFloat("MusicPref");
            MusicChannels.clip = Clip;
            MusicChannels.Play();
            return true;
        }
        return false;
    }

    public bool StopMusic(AudioClip Clip)
    {

        if (MusicChannels.clip == Clip)
        {
            MusicChannels.Stop();
            return true;
        }

        return false;
    }

    public bool PlayMusic(string Id)
    {
        if (MusicDictionary.ContainsKey(Id))
        {
            var tempRef = MusicDictionary[Id];
            return PlayMusic(tempRef.Audio);
        }
        return false;

    }

    public bool StopMusic(string Id)
    {
        if (MusicDictionary.ContainsKey(Id))
        {
            var tempRef = MusicDictionary[Id];
            return StopMusic(tempRef.Audio);
        }
        return false;
    }

    public void UpdateVolume()
    {

        foreach (var channel in Channels)
        {
            channel.volume = PlayerPrefs.GetFloat("SfxPref");
        }
        MusicChannels.volume = PlayerPrefs.GetFloat("MusicPref");
    }
}
