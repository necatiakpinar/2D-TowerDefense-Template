using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Data;
using NecatiAkpinar.Enemies;
using NecatiAkpinar.Misc;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private ContainerLevelData _levels;
        private WaitForSeconds _waitEnemySpawnSpeed;

        private LevelData _currentLevel;
        private int _currentLevelIndex;
        private int _currentSpawnedEnemyCount;

        private List<BaseEnemy> _spawnedEnemies;
        private bool _isLevelFinished;

        private void OnEnable()
        {
            EventManager.GetTotalEnemyAmount += GetTotalEnemyAmount;
            EventManager.OnEnemyDied += OnEnemyDied;
            EventManager.OnLevelFinished += OnLevelFinished;
        }
        private void OnDisable()
        {
            EventManager.GetTotalEnemyAmount -= GetTotalEnemyAmount;
            EventManager.OnEnemyDied -= OnEnemyDied;
            EventManager.OnLevelFinished -= OnLevelFinished;
        }

        private void Awake()
        {
            _currentLevelIndex = Player.GameplayData.CurrentLevelIndex;

            if (_levels.Levels == null)
                return;

            if (_levels.Levels.Count <= _currentLevelIndex)
            {
                Player.GameplayData.ChangeCurrentLevelIndex(0);
                _currentLevelIndex = Player.GameplayData.CurrentLevelIndex;
            }

            _currentLevel = _levels.Levels[_currentLevelIndex];

            _waitEnemySpawnSpeed = new WaitForSeconds(_currentLevel.EnemySpawnSpeed);
            _currentSpawnedEnemyCount = 0;
            _spawnedEnemies = new List<BaseEnemy>();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            if (_currentSpawnedEnemyCount == _currentLevel.TargetEnemyCount)
                yield break;

            _currentSpawnedEnemyCount++;
            var enemy = EnemyPoolManager.Instance.SpawnFromPool(EnemyType.BasicEnemy, Vector3.zero, Quaternion.identity);

            if (!_spawnedEnemies.Contains(enemy))
                _spawnedEnemies.Add(enemy);

            yield return _waitEnemySpawnSpeed;
            if (_isLevelFinished)
                yield break;

            StartCoroutine(SpawnEnemy());
        }

        private int GetTotalEnemyAmount()
        {
            return _currentLevel.TargetEnemyCount;
        }
        
        private void OnEnemyDied(BaseEnemy enemyToRemove)
        {
            if (_spawnedEnemies.Contains(enemyToRemove))
                _spawnedEnemies.Remove(enemyToRemove);

            CurrencyProviderData currencyProvider = GameReferences.Instance.EnemyCurrencyProvider.GetCurrencyProvider((GameElementType)enemyToRemove.EnemyType);
            Player.GameplayData.OwnedCurrencies.IncreaseCurrency(currencyProvider.CurrencyType, currencyProvider.CurrencyAmount);
            Player.SaveDataToDisk();

            if (_currentSpawnedEnemyCount == _currentLevel.TargetEnemyCount && _spawnedEnemies.Count == 0)
                EventManager.OnLevelFinished?.Invoke(true);
        }

        private void OnLevelFinished(bool isWin)
        {
            _isLevelFinished = true;
            StopEnemyMovement();

            if (isWin)
            {
                Player.GameplayData.IncreaseLevel();
                Player.SaveDataToDisk();
            }
        }

        private void StopEnemyMovement()
        {
            BaseEnemy enemy;
            for (int i = 0; i < _spawnedEnemies.Count; i++)
            {
                enemy = _spawnedEnemies[i];
                if (enemy)
                    enemy.MovementController.StopMovement();
            }
        }
    }
}