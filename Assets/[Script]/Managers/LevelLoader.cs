using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transtiontime = 1f;

    public void buttonTransition()
    {
        LoadNextLevel();
        Debug.Log("Button Pressed");
    }
    public void GoToLevelSelection()
    {
        AudioManager.Instance.PlayAudio("ButtonClick");
        StartCoroutine(LevelSelection());
        SceneManager.LoadScene("LevelSelectionScene");
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
        SceneManager.LoadScene("LevelSelection2");

    }
    public void BackToMenuButton()
    {
        StartCoroutine(BackToMenu());
        SceneManager.LoadScene("MainMenuScene");

    }

    public void GoToEncyclopedia()
    {
        SceneManager.LoadScene("Enemy_ Encyclopedia");
    }
    
    public IEnumerator LevelSelection()
    {
        //transition.SetTrigger("Start");
        AudioManager.Instance.PlayAudio("ButtonClick");
        yield return new WaitForSeconds(transtiontime);
        SceneManager.LoadScene("LevelSelection1");
    }


    public IEnumerator LoadLevel()
    {
        //transition.SetTrigger("Start");
        yield return new WaitForSeconds(transtiontime);
        SceneManager.LoadScene("LevelSelection2");
    }
    public IEnumerator BackToMenu()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transtiontime);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoToTowerEncyclopedia()
    {
        SceneManager.LoadScene("Tower_ Encyclopedia");
    }
    public void GoToElements()
    {
        SceneManager.LoadScene("Elements");
    }
}
