using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Projectiles;
using NecatiAkpinar.Data;
using NecatiAkpinar.Enemies;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using TMPro;
using UnityEngine;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BaseTower : BaseActor
    {
        [SerializeField] protected BaseTowerData _data;
        [SerializeField] protected BaseProjectile _projectilePF;
        [SerializeField] private TextMeshPro _levelTextLabel;

        protected TowerStatsData _statsData;
        public TowerStateType _towerState;
        private int _level;

        public BaseEnemy _targetEnemy;
        public List<BaseEnemy> _detectedEnemies;
        public bool _isShooting;
        private int _targetEnemyIndex;
        protected WaitForSeconds _waitForNextShot;

        public BaseTowerData Data => _data;
        public TowerStateType TowerState => _towerState;
        public int Level => _level;

        public ITowerPlacable TowerPlacable;

        private void OnEnable()
        {
            EventManager.OnEnemyDied += RemoveEnemyFromTargets;
            EventManager.OnLevelFinished += StopShooting;
        }

        private void OnDisable()
        {
            EventManager.OnEnemyDied -= RemoveEnemyFromTargets;
            EventManager.OnLevelFinished -= StopShooting;
        }

        public void Init(TowerStateType towerState, int level, ITowerPlacable towerPlacable)
        {
            SetState(towerState);
            SetLevel(level);

            _statsData = _data.GetStatsAmountByLevel(level);
            TowerPlacable = towerPlacable;
            _detectedEnemies = new List<BaseEnemy>();
            _isShooting = false;
            _targetEnemyIndex = 0;
            _waitForNextShot = new WaitForSeconds(_statsData.AttackSpeed);

            UpdateLevelLabel();
        }

        public void SetState(TowerStateType towerState)
        {
            _towerState = towerState;

            if (_towerState == TowerStateType.OnField)
                CheckCanShoot();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            BaseEnemy enemy = col.GetComponent<BaseEnemy>();

            if (enemy && !_detectedEnemies.Contains(enemy))
            {
                _detectedEnemies.Add(enemy);

                if (_towerState != TowerStateType.OnField)
                    return;

                if (!_isShooting)
                {
                    _isShooting = true;
                    StartCoroutine(TryShoot());
                }
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            BaseEnemy enemy = col.GetComponent<BaseEnemy>();

            if (enemy && _detectedEnemies.Contains(enemy))
            {
                _detectedEnemies.Remove(enemy);
                if (_detectedEnemies.Count == 0)
                {
                    _isShooting = false;
                }
            }
        }

        private void CheckCanShoot()
        {
            if (!_isShooting && _detectedEnemies.Count > 0)
            {
                _isShooting = true;
                StartCoroutine(TryShoot());
            }
        }

        private IEnumerator TryShoot()
        {
            if (!_isShooting)
                yield break;

            yield return StartCoroutine(Shoot());
            StartCoroutine(TryShoot());
        }

        protected virtual IEnumerator Shoot()
        {
            yield break;
        }

        protected BaseEnemy GetFirstTargetEnemy()
        {
            BaseEnemy targetEnemy;

            if (_detectedEnemies.Count == 0)
                return null;

            targetEnemy = _detectedEnemies[_targetEnemyIndex];
            while (targetEnemy == null)
            {
                if (_targetEnemyIndex < _detectedEnemies.Count - 1)
                {
                    _targetEnemyIndex++;
                    targetEnemy = _detectedEnemies[_targetEnemyIndex];
                }
                else
                    break;
            }

            return targetEnemy;
        }

        private void RemoveEnemyFromTargets(BaseEnemy killedEnemy)
        {
            if (_detectedEnemies.Contains(killedEnemy))
            {
                _detectedEnemies.Remove(killedEnemy);
            }
        }

        public void IncreaseLevel(int increaser = 1)
        {
            _level += increaser;

            _statsData = _data.GetStatsAmountByLevel(_level);
            UpdateLevelLabel();
        }

        public void SetLevel(int newLevel)
        {
            _level = newLevel;
            _statsData = _data.GetStatsAmountByLevel(_level);
            UpdateLevelLabel();
        }

        public void UpdateLevelLabel()
        {
            _levelTextLabel.text = $"Level: {_level + 1}";
        }

        public virtual void ReturnToPool()
        {
            TowerPoolManager.Instance.ReturnToPool(_data.TowerType, this);
        }

        public void StopShooting(bool isWin)
        {
            _isShooting = false;
        }
        
    }
}