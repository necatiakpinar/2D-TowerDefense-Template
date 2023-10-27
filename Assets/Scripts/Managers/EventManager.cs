using System;
using NecatiAkpinar.Data;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enemies;
using NecatiAkpinar.TowerDeck;
using NecatiAkpinar.Utils;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public static class EventManager
    {
        public static Action<BaseEnemy> OnEnemyDied;
        public static Action OnEnemyDiedEvent;
        public static Action<TowerType, bool> OnTowerPurchased;
        public static Action<BaseTower> OnPurchasedTowerCreated;
        public static Action<TowerNode, BaseTower> OnTowerPlacedOnNode;
        public static Action<TowerNode, BaseTower> OnTowerReleasedFromNode;
        public static Action<TowerSlot, BaseTower> OnTowerPlacedOnSlot;
        public static Action<TowerSlot, BaseTower> OnTowerReleasedFromSlot;

        public static Action<BaseTower> OnTowerUpdated;
        public static Action OnTowerSelectedEvent;
        public static Action OnTowerDeSelectedEvent;
        public static Action<bool> OnLevelFinished;
        public static Action<bool> OnLevelEndWindowActivated;

        public static Func<PathNode[]> GetPathNodes;
        public static Func<int> GetTotalEnemyAmount;
        public static Func<bool> HasCurrencyToPurchaseTower;
        public static Func<int> GetTowerPurchasePrice;
        public static Func<bool> HasEmptyTowerSlot;
        public static Func<TowerType, Vector3, BaseTower> CreateTower;
    }
}