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
    public class ScoreWidget : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _scoreLabel;

        private void OnEnable()
        {
            EventManager.OnEnemyDiedEvent += UpdateScoreLabel;
        }

        private void OnDisable()
        {
            EventManager.OnEnemyDiedEvent -= UpdateScoreLabel;
        }

        private void Start()
        {
            UpdateScoreLabel();
        }

        private void UpdateScoreLabel()
        {
            _scoreLabel.text = $"Total Killed Enemy= {Player.GameplayData.TotalKilledEnemies.ToString()}";
        }
    }
}