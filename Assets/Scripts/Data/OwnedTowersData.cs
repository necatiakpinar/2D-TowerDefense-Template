using System;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Misc;
using UnityEditorInternal;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [Serializable]
    public class OwnedTowersData
    {
        // [SerializeField] private List<OwnerTowerData> _placedTowerDatas = new List<OwnerTowerData>();
        // [SerializeField] private List<OwnerTowerData> _deckTowerDatas = new List<OwnerTowerData>();

        [SerializeField] private SerializableDictionary<TowerStateType, List<OwnerTowerData>> _ownedTowers = new SerializableDictionary<TowerStateType, List<OwnerTowerData>>();
        public SerializableDictionary<TowerStateType, List<OwnerTowerData>> OwnedTowers => _ownedTowers;

        public void CreateTowerStates()
        {
            if (!_ownedTowers.ContainsKey(TowerStateType.OnField))
            {
                _ownedTowers.Add(TowerStateType.OnField, new List<OwnerTowerData>());
            }
            
            if (!_ownedTowers.ContainsKey(TowerStateType.OnDeck))
                _ownedTowers.Add(TowerStateType.OnDeck, new List<OwnerTowerData>());
        }
        public void AddOwnedTower(TowerStateType stateType, OwnerTowerData ownerTowerData)
        {
            if (!HasTowerOwned(stateType, ownerTowerData))
                return;

            _ownedTowers.GetValue(stateType).Add(ownerTowerData);
        }

        public bool HasTowerOwned(TowerStateType stateType, OwnerTowerData possibleOwnerTowerData)
        {
            OwnerTowerData ownerTowerData;
            List<OwnerTowerData> ownerTowerDatas = _ownedTowers.GetValue(stateType);
            for (int i = 0; i < ownerTowerDatas.Count; i++)
            {
                ownerTowerData = ownerTowerDatas[i];
                if (possibleOwnerTowerData.TowerPlacedNodeIndex == ownerTowerData.TowerPlacedNodeIndex)
                    return false;
            }

            return true;
        }

        public void RemoveOwnedTower(TowerStateType stateType, int towerNodeIndex)  
        {
            OwnerTowerData ownerTowerData;
            List<OwnerTowerData> ownerTowerDatas = _ownedTowers.GetValue(stateType);
            for (int i = 0; i < ownerTowerDatas.Count; i++)
            {
                ownerTowerData = ownerTowerDatas[i];
                if (ownerTowerData.TowerPlacedNodeIndex == towerNodeIndex)
                    ownerTowerDatas.Remove(ownerTowerData);
            }
        }

        public void UpdateOwnedTower(TowerStateType stateType, BaseTower placedTower)
        {
            OwnerTowerData ownerTowerData = GetOwnedTowerData(stateType, placedTower);
            if (ownerTowerData != null)
                ownerTowerData.TowerLevel = placedTower.Level;

        }

        public OwnerTowerData GetOwnedTowerData(TowerStateType stateType, BaseTower placedTower)
        {
            OwnerTowerData ownerTowerData;
            List<OwnerTowerData> ownerTowerDatas = _ownedTowers.GetValue(stateType);
            for (int i = 0; i < ownerTowerDatas.Count; i++)
            {
                ownerTowerData = ownerTowerDatas[i];
                if (ownerTowerData.TowerReference == placedTower)
                    return ownerTowerData;
            }

            return null;
        }

        public bool HasPurchasedTower()
        {
            List<OwnerTowerData> ownedDeckDatas = _ownedTowers.GetValue(TowerStateType.OnDeck);
            List<OwnerTowerData> ownedFieldDatas = _ownedTowers.GetValue(TowerStateType.OnField);

            if (ownedDeckDatas.Count == 0 && ownedFieldDatas.Count == 0)
                return false;

            return true;
        }
    }
}