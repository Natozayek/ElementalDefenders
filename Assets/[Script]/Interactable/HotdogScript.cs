using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotdogScript : Interactable
{
    AudioClip clip;
    bool isPlaying = false;
    protected override void OnInteraction()
    {
        if(!isPlaying)
            StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio()
    {
        isPlaying = true;
        AudioManager.Instance.PlayAudio(clip);
        yield return new WaitForSecondsRealtime(clip.length + 0.3f);
        isPlaying = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        clip = Resources.Load<AudioClip>("Audio/SoundEffect/wtf am i doing");
    }
}