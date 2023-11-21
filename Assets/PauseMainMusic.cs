using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMainMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyMusicOnLoad.Instance.GetComponent<AudioSource>().Pause();
        Destroy(DontDestroyMusicOnLoad.Instance.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
