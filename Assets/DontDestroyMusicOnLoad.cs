using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusicOnLoad : MonoBehaviour
{
    private static DontDestroyMusicOnLoad instance =  null;
    public static DontDestroyMusicOnLoad Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
