using DG.Tweening;
using UnityEngine;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BaseEnemyData : ScriptableObject
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _totalHealth = 100;
        [SerializeField] private float _movementDuration = 1.0f;
        [SerializeField] private Ease _movementEase = Ease.Linear;

        public EnemyType EnemyType => _enemyType;
        public int TotalHealth => _totalHealth;
        public float MovementDuration => _movementDuration;
        public Ease MovementEase => _movementEase;
    }
}