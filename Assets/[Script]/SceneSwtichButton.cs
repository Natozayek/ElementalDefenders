using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwtichButton : Button
{
    [SerializeField]
    private bool useIndex = false;

    [SerializeField]
    private int sceneIndex = 0;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private LoadSceneMode mode = LoadSceneMode.Single;
    protected override void Awake()
    {
        onClick.AddListener(SwitchScene);   
    }

    private void SwitchScene()
    {
        StartCoroutine(Swtich());
    }

    IEnumerator Swtich()
    {
        AudioManager.Instance.PlayAudio("ButtonClick");
        yield return new WaitForFixedUpdate();
        if(useIndex)
            SceneManager.LoadScene(sceneIndex, mode);
        else
            SceneManager.LoadScene(sceneName, mode);
    }
}
