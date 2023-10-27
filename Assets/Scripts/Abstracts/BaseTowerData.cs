using System;
using System.Collections.Generic;
using NecatiAkpinar.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Abstracts
{
    public class BaseTowerData : ScriptableObject
    {
        [SerializeField] private TowerType _towerType;
        [SerializeField] protected List<TowerStatsData> _statsByLevel;
        public TowerType TowerType => _towerType;

        public TowerStatsData GetStatsAmountByLevel(int towerLevel)
        {
            TowerStatsData statsData;
            for (int i = 0; i < _statsByLevel.Count; i++)
            {
                statsData = _statsByLevel[i];
                if (_statsByLevel[i].TowerLevel == towerLevel)
                    return statsData;
            }

            return null;
        }
    }
}