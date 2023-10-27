using System;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Data;
using NecatiAkpinar.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Enemies
{
    public class BaseEnemy : BaseActor
    {
        [SerializeField] private BaseEnemyData _data;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private MovementController _movementController;

        public EnemyType EnemyType => _data.EnemyType;
        public MovementController MovementController => _movementController;

        public int _currentHealth = 0;

        public void Init()
        {
            _movementController = new MovementController(this, EventManager.GetPathNodes(), _data.MovementDuration, _data.MovementEase);
            _movementController.ActivateMovement();

            _currentHealth = _data.TotalHealth;
        }

        public void TakeDamage(int damageTaken)
        {
            _currentHealth -= damageTaken;

            UpdateHealthBar();

            if (_currentHealth <= 0)
                Die();
        }

        private void UpdateHealthBar()
        {
            float healthPercent = _currentHealth / (float)_data.TotalHealth;
            _spriteRenderer.material.SetFloat("_FillAmount", healthPercent);
        }

        private void Die()
        {
            Player.GameplayData.IncreaseKilledEnemyAmount(1);
            EventManager.OnEnemyDied?.Invoke(this); //Order is important, after calculations has been done normal event version must be fired!
            EventManager.OnEnemyDiedEvent?.Invoke();


            EnemyPoolManager.Instance.ReturnToPool(_data.EnemyType, this);
        }

        public override void ResetActor()
        {
            base.ResetActor();
            transform.localPosition = Vector3.zero;
        }
    }
}