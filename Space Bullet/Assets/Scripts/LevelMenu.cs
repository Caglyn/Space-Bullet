using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    private int levelsUnlocked;
    public Button[] buttons;

    private void Awake()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for(int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void OpenLastSavedLevel()
    {
        String path = Application.persistentDataPath + "/unlockedLevels.bin";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int lastSavedLevel = (int) formatter.Deserialize(stream);
            SceneManager.LoadScene(lastSavedLevel);
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return;
        }
    }

    public void ResetUnlockedLevels()
    {
        PlayerPrefs.DeleteAll();
    }
}
