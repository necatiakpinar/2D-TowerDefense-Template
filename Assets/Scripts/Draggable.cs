using System;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using NecatiAkpinar.TowerDeck;
using NecatiAkpinar.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private ITowerPlacable _towerPlacable;
    private bool _isDragging = false;

    private BaseTower _baseTower;
    private Camera _camera;

    public BaseTower BaseTower => _baseTower;
    private ITowerPlacable _placedTowerPlacable;
    private List<ITowerPlacable> _collectedTowerPlacables = new List<ITowerPlacable>();
    private List<Draggable> _collectedDraggables = new List<Draggable>();
    private TowerDestroyer _towerDestroyer;

    public void Start()
    {
        _camera = Camera.main;
        _baseTower = GetComponentInParent<BaseTower>();
        _towerPlacable = _baseTower.TowerPlacable;

        if (_towerPlacable != null)
            _towerPlacable.PlaceTower(_baseTower, ref _placedTowerPlacable);
    }

    private void Update()
    {
        if (_isDragging && _baseTower)
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _baseTower.transform.position = mousePos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;

        if (_placedTowerPlacable != null)
            _placedTowerPlacable.ReleaseTower();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        if (_towerDestroyer)
        {
            _placedTowerPlacable.ReleaseTower();
            _towerDestroyer.DestroyTower(this.BaseTower);
            return;
        }

        if (TryMergeOrSwapTowers()) return;

        TryPlaceTower();
    }

    private bool TryMergeOrSwapTowers()
    {
        if (_baseTower != null && _collectedDraggables.Count > 0)
        {
            for (int i = _collectedDraggables.Count - 1; i > -1; i--)
            {
                Draggable collectedDraggable = _collectedDraggables[i];
                if (_baseTower.Level == collectedDraggable.BaseTower.Level && _baseTower.Data.TowerType == collectedDraggable.BaseTower.Data.TowerType)
                {
                    MergeTower(collectedDraggable);
                    _collectedDraggables.Clear();
                    break;
                }

                SwapTower(collectedDraggable);
                _collectedDraggables.Clear();
                break;
            }

            _collectedTowerPlacables.Clear();
            return true;
        }

        return false;
    }

    private void TryPlaceTower()
    {
        if (_collectedTowerPlacables.Count > 0)
        {
            int lastTowerPlacableIndex = _collectedTowerPlacables.Count - 1;
            _towerPlacable = _collectedTowerPlacables[lastTowerPlacableIndex];
            _baseTower.transform.position = _towerPlacable.GetTransform().position;

            if (_baseTower.TowerState == TowerStateType.OnSelected)
            {
                _towerPlacable.PlaceTower(_baseTower, ref _placedTowerPlacable);
                _collectedTowerPlacables.Clear();
            }
        }
        else if (_placedTowerPlacable != null)
        {
            _placedTowerPlacable.PlaceTower(_baseTower, ref _placedTowerPlacable);
        }
    }

    public void SwapTower(Draggable swappedDraggable)
    {
        ITowerPlacable thisPlacable = _placedTowerPlacable;
        ITowerPlacable swappedPlacable = swappedDraggable._placedTowerPlacable;

        BaseTower swappedTower = swappedPlacable.GetTower();
        swappedDraggable._placedTowerPlacable.ReleaseTower();
        swappedDraggable._placedTowerPlacable.PlaceTower(_baseTower, ref _placedTowerPlacable);

        thisPlacable.ReleaseTower();
        thisPlacable.PlaceTower(swappedTower, ref swappedDraggable._placedTowerPlacable);
    }

    private void MergeTower(Draggable mergedDraggable)
    {
        mergedDraggable.BaseTower.IncreaseLevel();
        EventManager.OnTowerUpdated?.Invoke(mergedDraggable.BaseTower);
        _baseTower.ReturnToPool();

        if (_towerPlacable != null)
            _towerPlacable.ReleaseTower();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ITowerPlacable towerPlacable = other.GetComponent<ITowerPlacable>();
        Draggable draggableTower = other.GetComponent<Draggable>();
        TowerDestroyer towerDestroyer = other.GetComponent<TowerDestroyer>();

        if (towerPlacable != null && !_collectedTowerPlacables.Contains(towerPlacable))
            _collectedTowerPlacables.Add(towerPlacable);

        if (draggableTower != null && !_collectedDraggables.Contains(draggableTower))
            _collectedDraggables.Add(draggableTower);

        if (towerDestroyer != null)
            _towerDestroyer = towerDestroyer;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ITowerPlacable towerPlacable = other.GetComponent<ITowerPlacable>();
        Draggable draggableTower = other.GetComponent<Draggable>();
        TowerDestroyer towerDestroyer = other.GetComponent<TowerDestroyer>();

        if (towerPlacable != null && _collectedTowerPlacables.Contains(towerPlacable))
            _collectedTowerPlacables.Remove(towerPlacable);

        if (draggableTower != null && _collectedDraggables.Contains(draggableTower))
            _collectedDraggables.Remove(draggableTower);

        if (towerDestroyer != null)
            _towerDestroyer = null;
    }
}