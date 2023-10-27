using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Data;
using NecatiAkpinar.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NecatiAkpinar.UI.Widget
{
    public class GameplayTopBarWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentLevelLabel;
        [SerializeField] private TMP_Text _nextLevelLabel;
        [SerializeField] private Slider _progressSlider;

        private float _progressBarStartingValue = 0.0f;
        private float _progressBarEndingValue; 
        
        private GameplayData _gameplayData;
        private int _currentKilledEnemyAmount = 0;

        private void OnEnable()
        {
            EventManager.OnEnemyDiedEvent += UpdateProgressBar;
        }

        private void OnDestroy()
        {
            EventManager.OnEnemyDiedEvent -= UpdateProgressBar;
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _gameplayData = Player.GameplayData;

            _currentLevelLabel.text = (_gameplayData.CurrentLevelIndex + 1).ToString();
            _nextLevelLabel.text = (_gameplayData.CurrentLevelIndex + 2).ToString();

            _progressBarEndingValue = EventManager.GetTotalEnemyAmount();

            _progressSlider.minValue = _progressBarStartingValue;
            _progressSlider.maxValue = _progressBarEndingValue;
        }

        private void UpdateProgressBar()
        {
            _currentKilledEnemyAmount++;
            _progressSlider.value = _currentKilledEnemyAmount;
        }
    }
}