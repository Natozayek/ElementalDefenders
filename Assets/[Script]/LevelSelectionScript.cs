using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelSelectionScript : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public GameObject display;
        public string level;
    }
    [Header("UI Element")]
    [SerializeField]
    private Button leftButton;

    [SerializeField]
    private Button rightButton;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private TMP_Text levelText;

    [SerializeField]
    private TMP_Text scoreText;


    [SerializeField]
    private GameObject lockIcon;

    [SerializeField]
    private GameObject SettingPanel;



    [Header("Level")]
    [SerializeField]
    private List<Level> levelDisplay;

    [SerializeField]
    private int levelProgress;

    [SerializeField]
    private int currentSelectedLevel;



    public static string selectedLevel = null;

    // Start is called before the first frame update
    void Start()
    {
        currentSelectedLevel = 0;
        levelProgress = SaveManager.Load();

        leftButton.onClick.AddListener(DecreaseSelectedLevel);
        leftButton.onClick.AddListener(UpdateButtonState);

        rightButton.onClick.AddListener(IncreaseSelectedLevel);
        rightButton.onClick.AddListener(UpdateButtonState);

        startButton.onClick.AddListener(LoadLevel);

        levelDisplay[currentSelectedLevel].display.SetActive(true);
        UpdateButtonState();
    }

    void DecreaseSelectedLevel()
    {
        levelDisplay[currentSelectedLevel].display.SetActive(false);
        currentSelectedLevel--;
        if(currentSelectedLevel <= -1)
        {
            currentSelectedLevel = levelDisplay.Count - 1;
        }
        levelDisplay[currentSelectedLevel].display.SetActive(true);

        if (AudioManager.Instance)
        {
            AudioManager.Instance.PlayAudio("ButtonClick");
        }
    }

    void IncreaseSelectedLevel()
    {
        levelDisplay[currentSelectedLevel].display.SetActive(false);
        currentSelectedLevel++;
        if (currentSelectedLevel >= levelDisplay.Count)
        {
            currentSelectedLevel = 0;
        }
        levelDisplay[currentSelectedLevel].display.SetActive(true);

        if (AudioManager.Instance)
        {
            AudioManager.Instance.PlayAudio("ButtonClick");
        }
    }

    void UpdateButtonState()
    {
        //Debug.Log(currentSelectedLevel);
        levelText.text = (currentSelectedLevel + 1).ToString();
        scoreText.text = SaveManager.LoadScore(levelDisplay[currentSelectedLevel].level).ToString();

        var isLock = currentSelectedLevel > levelProgress;

        //Debug.Log("Unlock");
        lockIcon.SetActive(isLock);
        startButton.interactable = !isLock;

        startButton.gameObject.SetActive(!(currentSelectedLevel >= levelDisplay.Count - 1));
    }

    void LoadLevel()
    {
        selectedLevel = levelDisplay[currentSelectedLevel].level;
        SceneManager.LoadScene(selectedLevel);
    }

    public void ToggleSetting()
    {
        if (AudioManager.Instance)
        {
            AudioManager.Instance.PlayAudio("ButtonClick");
        }
        SettingPanel.SetActive(!SettingPanel.activeInHierarchy);
    }
}
