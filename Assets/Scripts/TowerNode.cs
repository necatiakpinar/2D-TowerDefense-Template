using System;
using DG.Tweening;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Utils
{
    public class TowerNode : MonoBehaviour, ITowerPlacable
    {
        [SerializeField] private bool _isFull;
        [SerializeField] Color _targetColor = Color.yellow;
        [SerializeField] private float _duration = 0.5f;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        private BaseTower _baseTower;

        private Color _startColor;
        private Color _nodeStartingColor;

        private void OnEnable()
        {
            EventManager.OnTowerSelectedEvent += PlayHighlightEffect;
            EventManager.OnTowerDeSelectedEvent += StopHighlightEffect;
        }

        private void OnDisable()
        {
            EventManager.OnTowerSelectedEvent -= PlayHighlightEffect;
            EventManager.OnTowerDeSelectedEvent -= StopHighlightEffect;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _nodeStartingColor = _spriteRenderer.color;
            _startColor = _nodeStartingColor;
        }

        private void DisableVisuals()
        {
            _spriteRenderer.enabled = false;
        }

        public void PlaceTower(BaseTower tower, ref ITowerPlacable towerPlacable)
        {
            towerPlacable = this;
            _baseTower = tower;
            _baseTower.TowerPlacable = this;
            _baseTower.SetState(TowerStateType.OnField);
            var towerTransform = _baseTower.transform;
            towerTransform.parent = transform;
            towerTransform.localPosition = Vector3.zero;
            _isFull = true;
            _collider.enabled = false;
            EventManager.OnTowerDeSelectedEvent?.Invoke();
            EventManager.OnTowerPlacedOnNode?.Invoke(this, tower);
        }

        public void ReleaseTower()
        {
            if (_baseTower == null)
                return;
            
            EventManager.OnTowerReleasedFromNode?.Invoke(this, _baseTower);
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

        private void PlayHighlightEffect()
        {
            _spriteRenderer.DOColor(_targetColor, _duration)
                .OnComplete(() =>
                {
                    SwapColors();
                    PlayHighlightEffect();
                });
        }

        private void StopHighlightEffect()
        {
            _spriteRenderer.DOKill(false);
            _spriteRenderer.color = _nodeStartingColor;
        }
        void SwapColors()
        {
            Color temp = _startColor;
            _startColor = _targetColor;
            _targetColor = temp;
        }
    }
}