using System;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Misc;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Data
{
    [Serializable]
    public class GameplayData
    {
        [SerializeField] private int _currentLevelIndex;
        [SerializeField] private int _totalKilledEnemies;
        
        [SerializeField] private OwnedTowersData _ownedTowers;
        [SerializeField] private OwnedCurrenciesData _ownedCurrencies;

        private int _levelIncreaser = 1;
        public int CurrentLevelIndex => _currentLevelIndex;
        public int TotalKilledEnemies => _totalKilledEnemies;

        public OwnedCurrenciesData OwnedCurrencies => _ownedCurrencies;
        public OwnedTowersData OwnedTowers => _ownedTowers;
        
        public void IncreaseKilledEnemyAmount(int collectedAmount)
        {
            _totalKilledEnemies += collectedAmount;
        }

        public void IncreaseLevel()
        {
            _currentLevelIndex += _levelIncreaser;
        }

        public void ChangeCurrentLevelIndex(int newLevelIndex)
        {
            _currentLevelIndex = newLevelIndex;
        }

        public void ChangeTotalKilledEnemyAmount(int newKilledEnemyAmount)
        {
            _totalKilledEnemies = newKilledEnemyAmount;
        }

        public void ChangeCoinCurrencyAmount(int newCoinCurrencyAmount)
        {
            OwnedCurrencies.IncreaseCurrency(CurrencyType.Coin, newCoinCurrencyAmount);
        }
        
    }
}