using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class PlayerManager : MonoBehaviour
{
    static public UnityEvent<float> UpdateMoney = new UnityEvent<float>();

    [Header("PlayerResourcesData")]
    public int Gold;
    public int Gems;
    public int GemsFire;
    public int GemsWater;
    public int GemsEarth;
    public int Score;


    [Header("PlayerGameData")]
    public int Lifes;
    public int Wave;
    public int WaveTotal;
    public int WavesEnemiesKilled = 0;

    [Header("ResorceTextDisplayer")]
    public TMP_Text TextGold;
    public TMP_Text TextGems;
    public TMP_Text TextWaves;
    public TMP_Text TextWavesTotal;
    public TMP_Text TextGemsFire;
    public TMP_Text TextGemsWater;
    public TMP_Text TextGemsEarth;
    public TMP_Text TextHealth;

    SpawnerManager spawnerManager;


    // Start is called before the first frame update
    void Start()
    {
        spawnerManager = FindObjectOfType<SpawnerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
        if(Lifes <= 0)
        {
            GameOver(true);
        }
    }

    void UpdateStats()
    {
        TextGold.text = Gold.ToString();
        TextGems.text = Gems.ToString();
        TextWaves.text = (spawnerManager.currentWave + 1).ToString();
        TextWavesTotal.text =spawnerManager.Waves.Count.ToString();
        TextGemsFire.text = GemsFire.ToString();
        TextGemsWater.text = GemsWater.ToString();
        TextGemsEarth.text = GemsEarth.ToString();
        TextHealth.text = Lifes.ToString();
    }

    public void ModifyGold(int ammount)
    {
        Gold += ammount;
        UpdateMoney.Invoke(Gold);
    }
    public void ModifyGems(int ammount)
    {
        Gems += ammount;
    }

    public void AddElementalGem(int index, int ammout)
    {
        switch (index)
        {
            case 0:
                GemsFire += ammout;
                break;
            case 1:
                GemsWater += ammout;
                break;
            case 2:
                GemsEarth += ammout;
                break;
        }
    }

    public void RandomResourceGain(int index)
    {
        int Resource = 0;
        if(index == 0) {  Resource = 0; }
        else if(index == 1) { Resource = 1; }
        else if(index == 2) { Resource = 2; }
        else if(index == 3 || index == 4 || index == 6){ Resource = 3; }
        else { Resource = 4; }



        switch (Resource)
        {
            case 0:
                AddElementalGem(Resource, 1);
                break;
            case 1:
                AddElementalGem(Resource, 1);
                break;
            case 2:
                AddElementalGem(Resource, 1);
                break;
            case 3:
                ModifyGems(1);
                break;
            case 4:
                break;
        }
    }

    public void GameOver(bool lose, int level)
    {
        SingletonPlayerData.Instance.didPlayerLose = lose;

        SaveManager.SaveScore(Score, gameObject.scene.name);
        if (lose)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            SaveManager.Save(level);
            SceneManager.LoadScene("WinEndScene");
        }
    }

    public void GameOver(bool lose)
    {
        SingletonPlayerData.Instance.didPlayerLose = lose;

        if (lose)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {

            SceneManager.LoadScene("WinEndScene");
        }
    }

    public void TakeDamage()
    {
        Lifes--;
        if (Lifes <= 0)
        {
            SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Additive);
        }
    }

    public void TakeDamage(int damage = 1)
    {
        Lifes -= damage;
        if(Lifes <= 0)
        {
            SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Additive);
        }
    }

    public void AddWave()
    {
        Wave++;
    }

    public void AddScore(int add)
    {
        Score += add;
    }
}
