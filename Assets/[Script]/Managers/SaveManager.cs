using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class SaveManager : MonoBehaviour
{
    static public void Save(int levelProgress)
    {
        PlayerPrefs.SetInt("LevelProgress", levelProgress);
        PlayerPrefs.Save();
    }

    static public void SaveScore(int score, string levelName)
    {
        if (LoadScore(levelName) < score)
        {
            PlayerPrefs.SetInt(levelName, score);
            PlayerPrefs.Save();
        }
    }

    static public int LoadScore(string levelName)
    {
        var score = PlayerPrefs.GetInt(levelName, 0);
        return score;
    }

    static public int Load()
    {
        var progress = PlayerPrefs.GetInt("LevelProgress", 0);
        return progress;
    }
}
