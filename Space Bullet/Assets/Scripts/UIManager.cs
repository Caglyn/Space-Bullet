using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _retryText;
    [SerializeField] private Text _levelCompletedText;
    [SerializeField] private Image _tutorialImage;
    [SerializeField] private Image[] _bulletIndicator;
    private Scene _scene;
    
    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();

        Texts();
    }

    public void OnLevelFail()
    {
        _retryText.gameObject.SetActive(true);
    }

    public void OnLevelComplete()
    {
        _levelCompletedText.gameObject.SetActive(true);
    }

    private void Texts()
    {
        if(_scene.buildIndex == 1)
        {
            _tutorialImage.gameObject.SetActive(true);
        }
        else
        {
            _tutorialImage.gameObject.SetActive(false);
        }

        _retryText.gameObject.SetActive(false);
        _retryText.text = "Level Failed. Press 'R' to restart.";

        _levelCompletedText.gameObject.SetActive(false);
        if(_scene.buildIndex == 16)
        {
            _levelCompletedText.text = "Game finished. You have completed all levels. Congratulations!";
        }
        else
            _levelCompletedText.text = "Level Completed. Press 'Enter' to continue.";
    }

    public void PassTutorial()
    {
        _tutorialImage.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnBulletFired(int bullet)
    {
        _bulletIndicator[bullet].gameObject.SetActive(false);
    }

    public void RestoreBullets()
    {
        for(int i = 0; i < 4; i++)
        {
            _bulletIndicator[i].gameObject.SetActive(true);
        }
    }
}
