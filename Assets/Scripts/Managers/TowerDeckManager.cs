using System;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Data;
using NecatiAkpinar.TowerDeck;
using NecatiAkpinar.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Managers
{
    public class TowerDeckManager : MonoBehaviour
    {
        [SerializeField] private TowerSlot _towerSlotPf;
        [SerializeField] private TowerDestroyer _towerDestroyer;
        [SerializeField] private Transform _towerSlotParent;
        [SerializeField] private int _deckXSize;
        [SerializeField] private int _deckYSize;
        [SerializeField] private float _deckXSpacing = 0.2f;
        [SerializeField] private float _deckYSpacing = 0.2f;
        private List<TowerSlot> _towerSlots;
        private List<BaseTower> _deckTowers;
        private int _currentPositionI;
        private float currentXPosition;
        private float currentYPosition;

        private void OnEnable()
        {
            EventManager.OnPurchasedTowerCreated += AddTowerToDeck;
            EventManager.OnTowerPlacedOnSlot += OnTowerPlacedOnSlot;
            EventManager.OnTowerReleasedFromSlot += OnTowerReleasedOnSlot;
            EventManager.OnTowerUpdated += OnTowerUpdated;
            
            EventManager.HasEmptyTowerSlot += HasEmptyTowerSlot;
        }

        private void OnDisable()
        {
            EventManager.OnPurchasedTowerCreated -= AddTowerToDeck;
            EventManager.OnTowerPlacedOnSlot -= OnTowerPlacedOnSlot;
            EventManager.OnTowerReleasedFromSlot -= OnTowerReleasedOnSlot;
            EventManager.OnTowerUpdated -= OnTowerUpdated;
            
            EventManager.HasEmptyTowerSlot -= HasEmptyTowerSlot;
        }

        private void Awake()
        {
            CreateTowerSlots();
            _deckTowers = new List<BaseTower>();
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

            List<OwnerTowerData> placedTowerDatas = ownedTowers.OwnedTowers.GetValue(TowerStateType.OnDeck);
            if (placedTowerDatas == null || placedTowerDatas.Count == 0)
                return;

            OwnerTowerData ownerTowerData;

            for (int i = 0; i < placedTowerDatas.Count; i++)
            {
                ownerTowerData = placedTowerDatas[i];
                CreateOwnedTower(ownerTowerData);
            }
        }

        private void CreateOwnedTower(OwnerTowerData ownerTowerData)
        {
            BaseTower ownedTower = EventManager.CreateTower(ownerTowerData.TowerType, Vector3.zero);
            ownerTowerData.TowerReference = ownedTower;
            ownedTower.Init(TowerStateType.OnField, ownerTowerData.TowerLevel, _towerSlots[ownerTowerData.TowerPlacedNodeIndex]);
        }

        private void CreateTowerSlots()
        {
            TowerSlot towerSlot;
            _towerSlots = new List<TowerSlot>();

            currentXPosition = 0;
            currentYPosition = _deckYSize;

            for (int y = _deckYSize; y > 0; y--)
            {
                for (int x = 0; x < _deckXSize; x++)
                {
                    //Garbage controller will be created at that position
                    if (y == 1 && x == _deckXSize - 1)
                    {
                        var towerDestroyer = Instantiate(_towerDestroyer, _towerSlotParent);
                        towerDestroyer.transform.localPosition = new Vector2(currentXPosition, currentYPosition);
                        towerDestroyer.name = $"{x} , {y} Destroyer";
                        break;
                    }

                    towerSlot = Instantiate(_towerSlotPf, _towerSlotParent);
                    towerSlot.transform.localPosition = new Vector2(currentXPosition, currentYPosition);
                    towerSlot.name = $"{x} , {y}";
                    _towerSlots.Add(towerSlot);
                    currentXPosition = currentXPosition + 1 + _deckXSpacing;
                }

                currentXPosition = 0;
                currentYPosition = currentYPosition - 1 - _deckYSpacing;
            }
        }

        private void AddTowerToDeck(BaseTower tower)
        {
            TowerSlot towerSlot = GetFirstAvailableSlot();
            if (towerSlot == null)
                return;

            tower.Init(TowerStateType.OnDeck, 0, towerSlot);
            _deckTowers.Add(tower);
        }

        private TowerSlot GetFirstAvailableSlot()
        {
            for (int i = 0; i < _towerSlots.Count; i++)
                if (!_towerSlots[i].IsFull())
                    return _towerSlots[i];

            return null;
        }

        private bool HasEmptyTowerSlot() => GetFirstAvailableSlot() != null;
        

        private void OnTowerPlacedOnSlot(TowerSlot placedTowerNode, BaseTower placedTower)
        {
            TowerType towerType = placedTower.Data.TowerType;
            int towerLevel = placedTower.Level;
            int towerNodeIndex = GetTowerSlotIndex(placedTowerNode);
            OwnerTowerData ownerTowerData = new OwnerTowerData(TowerStateType.OnDeck, towerType, towerLevel, towerNodeIndex, placedTower);

            Player.GameplayData.OwnedTowers.AddOwnedTower(TowerStateType.OnDeck, ownerTowerData);
            Player.SaveDataToDisk();
        }

        private void OnTowerReleasedOnSlot(TowerSlot placedTowerSlot, BaseTower placedTower)
        {
            int towerSlotIndex = GetTowerSlotIndex(placedTowerSlot);
            Player.GameplayData.OwnedTowers.RemoveOwnedTower(TowerStateType.OnDeck, towerSlotIndex);
            Player.SaveDataToDisk();
        }

        private void OnTowerUpdated(BaseTower updatedTower)
        {
            Player.GameplayData.OwnedTowers.UpdateOwnedTower(TowerStateType.OnDeck, updatedTower);
            Player.SaveDataToDisk();
        }

        private int GetTowerSlotIndex(TowerSlot towerNode)
        {
            for (int i = 0; i < _towerSlots.Count; i++)
                if (_towerSlots[i] == towerNode)
                    return i;

            return -1;
        }
    }
}