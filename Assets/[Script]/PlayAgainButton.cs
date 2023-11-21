using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainButton : Button
{
    protected override void Awake()
    {
        onClick.AddListener(Restart);
    }

    void Restart()
    {
        AudioManager.Instance.PlayAudio("ButtonClick");
        var level = LevelSelectionScript.selectedLevel;
        if (level != null)
            SceneManager.LoadScene(level);
    }
}
