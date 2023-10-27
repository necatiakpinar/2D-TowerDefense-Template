using System;
using NecatiAkpinar.Misc;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [Serializable]
    public class OwnedCurrenciesData
    {
        [SerializeField] private SerializableDictionary<CurrencyType, int> _currencies = new SerializableDictionary<CurrencyType, int>();
        
        public int GetCurrencyAmount(CurrencyType currencyType)
        {
            if (_currencies.ContainsKey(currencyType))
                return _currencies.GetValue(currencyType);

            Debug.LogError("Currency does not exist!");
            return 0;
        }

        public void IncreaseCurrency(CurrencyType currencyType, int collectedAmount)
        {
            if (_currencies.ContainsKey(currencyType))
            {
                int currentValue = _currencies.GetValue(currencyType);
                _currencies.SetValue(currencyType, currentValue + collectedAmount);
            }
            else
            {
                _currencies.Add(currencyType, collectedAmount);
            }
        }
        
        public void DecreaseCurrency(CurrencyType currencyType, int decreaseAmount)
        {
            if (_currencies.ContainsKey(currencyType))
            {
                int currentValue = _currencies.GetValue(currencyType);
                _currencies.SetValue(currencyType, currentValue - decreaseAmount);
            }
            else
            {
                _currencies.Add(currencyType, decreaseAmount);
            }
        }

        public void ChangeCurrency(CurrencyType currencyType, int newCurrencyAmount)
        {
            if (_currencies.ContainsKey(currencyType))
                _currencies.SetValue(currencyType, newCurrencyAmount);
        }
    }
}