using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingSceneScript : MonoBehaviour
{
    public void CloseScene()
    {
        SceneManager.UnloadSceneAsync("SettingScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }

    public void CloseCreditScene()
    {
        SceneManager.UnloadSceneAsync("CreditScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }
}
