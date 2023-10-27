using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Data
{
    [Serializable]
    public class CurrencyProviderData
    {
        [SerializeField] private GameElementType _providerElementType;
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private int _currencyAmount;

        public GameElementType ProviderElementType => _providerElementType;
        public CurrencyType CurrencyType => _currencyType;
        public int CurrencyAmount => _currencyAmount;
    }

    public abstract class BaseCurrencyProvider : ScriptableObject
    {
        [SerializeField] private List<CurrencyProviderData> _currencyProviders;

        public CurrencyProviderData GetCurrencyProvider(GameElementType providerType)
        {
            CurrencyProviderData currencyProdiver;
            for (int i = 0; i < _currencyProviders.Count; i++)
            {
                currencyProdiver = _currencyProviders[i];
                if (currencyProdiver.ProviderElementType == providerType)
                    return currencyProdiver;
            }

            return null;
        }
    }
}