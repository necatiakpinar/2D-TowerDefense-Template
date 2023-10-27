using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Data;
using NecatiAkpinar.Utils;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private List<TowerNode> _towerNodes;

        private readonly int _towerPrice = 4;

        private void OnEnable()
        {
            EventManager.OnTowerPurchased += CreateTowerOnPurchased;
            EventManager.HasCurrencyToPurchaseTower += HasCurrencyToPurchaseTower;
            EventManager.OnTowerPlacedOnNode += OnTowerPlacedOnNode;
            EventManager.OnTowerReleasedFromNode += OnTowerReleasedOnNode;
            EventManager.OnTowerUpdated += OnTowerUpdated;
            EventManager.CreateTower += CreateTower;

            EventManager.GetTowerPurchasePrice += () => _towerPrice;
        }

        private void OnDisable()
        {
            EventManager.OnTowerPurchased -= CreateTowerOnPurchased;
            EventManager.HasCurrencyToPurchaseTower -= HasCurrencyToPurchaseTower;
            EventManager.OnTowerPlacedOnNode -= OnTowerPlacedOnNode;
            EventManager.OnTowerReleasedFromNode -= OnTowerReleasedOnNode;
            EventManager.OnTowerUpdated -= OnTowerUpdated;
            EventManager.CreateTower -= CreateTower;

            EventManager.GetTowerPurchasePrice -= () => _towerPrice;
        }

        private void Start()
        {
            TryLoadTowers();
        }

        private void TryLoadTowers()
        {
            OwnedTowersData ownedTowers = Player.GameplayData.OwnedTowers;
            if (ownedTowers == null)
                return;

            List<OwnerTowerData> placedTowersData = ownedTowers.OwnedTowers.GetValue(TowerStateType.OnField);
            if (placedTowersData == null || placedTowersData.Count == 0)
                return;

            OwnerTowerData ownerTowerData;

            for (int i = 0; i < placedTowersData.Count; i++)
            {
                ownerTowerData = placedTowersData[i];
                CreateOwnedTower(ownerTowerData);
            }
        }

        private bool HasCurrencyToPurchaseTower()
        {
            if (Player.GameplayData.OwnedCurrencies.GetCurrencyAmount(CurrencyType.Coin) >= _towerPrice)
                return true;

            return false;
        }

        private void CreateOwnedTower(OwnerTowerData ownerTowerData)
        {
            BaseTower ownedTower = CreateTower(ownerTowerData.TowerType, Vector3.zero);
            ownerTowerData.TowerReference = ownedTower;
            ownedTower.Init(TowerStateType.OnField, ownerTowerData.TowerLevel, _towerNodes[ownerTowerData.TowerPlacedNodeIndex]);
        }

        private void CreateTowerOnPurchased(TowerType towerType, bool isFree)
        {
            BaseTower tower = CreateTower(towerType, Vector3.zero);

            if (tower == null)
                return;

            EventManager.OnPurchasedTowerCreated?.Invoke(tower);
            if (!isFree)
            {
                Player.GameplayData.OwnedCurrencies.DecreaseCurrency(CurrencyType.Coin, _towerPrice);
                Player.SaveDataToDisk();
            }
        }

        private BaseTower CreateTower(TowerType towerType, Vector3 towerPosition)
        {
            BaseTower tower = TowerPoolManager.Instance.SpawnFromPool(towerType, towerPosition, Quaternion.identity);
            return tower;
        }

        private void OnTowerPlacedOnNode(TowerNode placedTowerNode, BaseTower placedTower)
        {
            TowerType towerType = placedTower.Data.TowerType;
            int towerLevel = placedTower.Level;
            int towerNodeIndex = GetTowerNodeIndex(placedTowerNode);
            OwnerTowerData ownerTowerData = new OwnerTowerData(TowerStateType.OnField, towerType, towerLevel, towerNodeIndex, placedTower);

            Player.GameplayData.OwnedTowers.AddOwnedTower(TowerStateType.OnField, ownerTowerData);
            Player.SaveDataToDisk();
        }

        private void OnTowerReleasedOnNode(TowerNode placedTowerNode, BaseTower placedTower)
        {
            int towerNodeIndex = GetTowerNodeIndex(placedTowerNode);
            Player.GameplayData.OwnedTowers.RemoveOwnedTower(TowerStateType.OnField, towerNodeIndex);
            Player.SaveDataToDisk();
        }

        private void OnTowerUpdated(BaseTower updatedTower)
        {
            Player.GameplayData.OwnedTowers.UpdateOwnedTower(TowerStateType.OnField, updatedTower);
            Player.SaveDataToDisk();
        }

        private int GetTowerNodeIndex(TowerNode towerNode)
        {
            for (int i = 0; i < _towerNodes.Count; i++)
                if (_towerNodes[i] == towerNode)
                    return i;

            return -1;
        }
    }
}