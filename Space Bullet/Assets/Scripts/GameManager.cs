using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int enemyNumber;

    private Scene _currentLevel;
    private UIManager _uiManager;
    private Indicator _indicator;

    private void Awake()
    {        
        _currentLevel = SceneManager.GetActiveScene();
        if(_currentLevel.buildIndex == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        DetermineEnemyNumber();

        _uiManager = FindObjectOfType<UIManager>();
        _uiManager.RestoreBullets();
        _indicator = FindObjectOfType<Indicator>();
    }

    private void Update()
    {
        LevelCompleted();
        RestartLevel();
    }

    private void DetermineEnemyNumber()
    {
        switch(_currentLevel.buildIndex)
        {
            case 3:
            case 7:
            case 11:
            case 15:
                enemyNumber = 2;
                break;
            case 4:
            case 8:
            case 12:
            case 16:
                enemyNumber = 3;
                break;
            case 0:
                break;
            default:
                enemyNumber = 1;
                break;
        }
    }

    public void EnemyDestroyed()
    {
        enemyNumber--;
    }

    public bool AreAllEnemiesDead()
    {
        return enemyNumber == 0;
    }

    public void RestartLevel()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(_currentLevel.buildIndex);
        }
    }

    public void LevelFailed()
    {
        Time.timeScale = 0;
        _uiManager.OnLevelFail();
    }

    public void LevelCompleted()
    {
        if(AreAllEnemiesDead())
        {
            Time.timeScale = 0;
            _uiManager.OnLevelComplete();

            if(_currentLevel.buildIndex >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", _currentLevel.buildIndex + 1);
                SaveLevel(_currentLevel.buildIndex + 1);
            }

            if(Input.GetKey(KeyCode.Return) && _currentLevel.buildIndex != 16)
            {
                SceneManager.LoadScene(_currentLevel.buildIndex + 1);
            }
        }
    }

    public void SaveLevel(int level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        String path = Application.persistentDataPath + "/unlockedLevels.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, level);
        stream.Close();
    }
}