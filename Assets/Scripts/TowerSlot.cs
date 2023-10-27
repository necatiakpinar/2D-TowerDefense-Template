using System;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using UnityEngine;

namespace NecatiAkpinar.TowerDeck
{
    public class TowerSlot : MonoBehaviour, ITowerPlacable
    {
        [SerializeField] private bool _isFull;

        public BaseTower _baseTower;
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public void PlaceTower(BaseTower tower, ref ITowerPlacable towerPlacable)
        {
            towerPlacable = this;
            _baseTower = tower;
            _baseTower.TowerPlacable = this;
            _baseTower.SetState(TowerStateType.OnDeck);
            var towerTransform = _baseTower.transform;
            towerTransform.parent = transform;
            towerTransform.localPosition = Vector3.zero;
            _isFull = true;
            _collider.enabled = false;
            EventManager.OnTowerDeSelectedEvent?.Invoke();
            EventManager.OnTowerPlacedOnSlot?.Invoke(this, tower);
        }

        public void ReleaseTower()
        {
            if (_baseTower == null)
                return;
            
            EventManager.OnTowerReleasedFromSlot?.Invoke(this, _baseTower);
            _baseTower.SetState(TowerStateType.OnSelected);
            _baseTower.TowerPlacable = null;
            _baseTower = null;
            _isFull = false;
            _collider.enabled = true;
            EventManager.OnTowerSelectedEvent?.Invoke();
        }

        public BaseTower GetTower()
        {
            return _baseTower;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public bool IsFull()
        {
            return _isFull;
        }
    }
}