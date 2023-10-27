using System;
using NecatiAkpinar.Abstracts;
using Unity.VisualScripting;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [Serializable]
    public class OwnerTowerData
    {
        [SerializeField] private TowerStateType _towerStateType;
        [SerializeField] private TowerType _towerType;
        [SerializeField] private int _towerLevel;
        [SerializeField] private int _towerPlacedNodeIndex;
        [SerializeField] private BaseTower _towerReference;

        public TowerStateType TowerStateType => _towerStateType;
        public TowerType TowerType => _towerType;

        public int TowerLevel
        {
            get => _towerLevel;
            set => _towerLevel = value;
        }

        public int TowerPlacedNodeIndex => _towerPlacedNodeIndex;

        public BaseTower TowerReference
        {
            get => _towerReference;
            set => _towerReference = value;
        }

        public OwnerTowerData(TowerStateType towerStateType, TowerType towerType, int towerLevel, int towerPlacedNodeIndex, BaseTower towerReference)
        {
            _towerStateType = towerStateType;
            _towerType = towerType;
            _towerLevel = towerLevel;
            _towerPlacedNodeIndex = towerPlacedNodeIndex;
            _towerReference = towerReference;
        }
    }
}