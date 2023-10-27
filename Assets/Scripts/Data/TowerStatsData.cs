using System;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [Serializable]
    public class TowerStatsData
    {
        [SerializeField] private int _towerLevel;
        [SerializeField] private int _damageAmount;
        [SerializeField] private float _attackSpeed;

        public int TowerLevel => _towerLevel;
        public int DamageAmount => _damageAmount;
        public float AttackSpeed => _attackSpeed;
    }
}