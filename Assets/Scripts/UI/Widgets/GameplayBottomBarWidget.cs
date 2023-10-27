using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Data;
using NecatiAkpinar.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NecatiAkpinar.UI.Widget
{
    public class GameplayBottomBarWidget : MonoBehaviour
    {
        [Header("Texts")] [SerializeField] private TMP_Text _towerLevelLabel;
        [SerializeField] private TMP_Text _towerPriceLabel;
        [SerializeField] private TMP_Text _totalCoinAmountLabel;

        [Header("Buttons")] [SerializeField] private Button _purchaseTowerButton;

        private void OnEnable()
        {
            EventManager.OnEnemyDiedEvent += SetTotalCoinLabel;
        }

        private void OnDisable()
        {
            EventManager.OnEnemyDiedEvent -= SetTotalCoinLabel;
        }

        private void Start()
        {
            _purchaseTowerButton.onClick.AddListener(TryPurchaseTower);

            SetTotalCoinLabel();
            SetPriceLabel();
        }

        private void SetTotalCoinLabel()
        {
            int totalCoin = Player.GameplayData.OwnedCurrencies.GetCurrencyAmount(CurrencyType.Coin);
            _totalCoinAmountLabel.text = $"Total Coin: {totalCoin}";
        }

        private void SetPriceLabel()
        {
            _towerPriceLabel.text = $"{EventManager.GetTowerPurchasePrice()} coin";
        }

        private void TryPurchaseTower()
        {
            if (!EventManager.HasCurrencyToPurchaseTower() || !EventManager.HasEmptyTowerSlot())
                return;

            TowerType randomTowerType = Player.GetRandomTowerType();
            EventManager.OnTowerPurchased?.Invoke(randomTowerType, false);

            SetTotalCoinLabel();
        }
    }
}