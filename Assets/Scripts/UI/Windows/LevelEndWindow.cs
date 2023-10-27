using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndWindow : BaseWindow
{
    [SerializeField] private TMP_Text titleLabel;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _retryButton;

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(LoadGameplayScene);
        _retryButton.onClick.AddListener(LoadGameplayScene);
    }

    private void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(LoadGameplayScene);
        _retryButton.onClick.RemoveListener(LoadGameplayScene);
    }
    
    public void Init(bool isPlayerWon)
    {
        if (isPlayerWon)
            WinScreen();
        else
            LoseScreen();
    }

    private void WinScreen()
    {
       _nextLevelButton.gameObject.SetActive(true);
       _retryButton.gameObject.SetActive(false);
       titleLabel.text = "Level Finished";
       titleLabel.color = Color.green;
    }

    private void LoseScreen()
    {
        _retryButton.gameObject.SetActive(true);
        _nextLevelButton.gameObject.SetActive(false);
        titleLabel.text = "Level Failed";
        titleLabel.color = Color.red;
    }
    private void LoadGameplayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
