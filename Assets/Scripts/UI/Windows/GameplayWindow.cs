using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Data;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.UI.Widget;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NecatiAkpinar.UI.Window
{
    public class GameplayWindow : BaseWindow
    {
        [Header("Widgets")] [SerializeField] private GameplayTopBarWidget _gameplayTopBarWidget;
        [SerializeField] private GameplayBottomBarWidget _gameplayBottomBarWidget;
        [SerializeField] private ScoreWidget _scoreWidget;

        [Header("Buttons")] [SerializeField] private Button _retryButton;

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveListener(OnRetryButtonClicked);
        }

        private void OnRetryButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}