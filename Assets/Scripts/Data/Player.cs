using System;
using NecatiAkpinar.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NecatiAkpinar.Data
{
    public static class Player
    {
        private static GameplayData _gameplayData;

        public static GameplayData GameplayData => _gameplayData;

        private static string _filePath =
            $"{Application.persistentDataPath}/{Constants.PLAYERDATA_FILENAME}.{Constants.SAVEFILE_EXTENSION}";

        public static void ResetData()
        {
            _gameplayData = new GameplayData();
            SaveDataToDisk();
        }

        public static void SaveDataToDisk()
        {
            var data = JsonUtility.ToJson(_gameplayData, true);
            if (FileHelper.WriteToFile(_filePath, data))
            {
                //Debug.Log("GameplayData successfully saved.");
            }
            else
            {
                Debug.LogWarning("GameplayData could not be saved.");
            }
        }

        public static void LoadSaveDataFromDisk()
        {
            GameplayData playerData;

            if (FileHelper.LoadFromFile(_filePath, out string diskData))
            {
                try
                {
                    playerData = JsonUtility.FromJson<GameplayData>(diskData);
                    playerData.OwnedCurrencies.IncreaseCurrency(CurrencyType.Coin, 0);
                    playerData.OwnedTowers.CreateTowerStates();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    playerData = new GameplayData();
                    playerData.OwnedCurrencies.IncreaseCurrency(CurrencyType.Coin, 0);
                    playerData.OwnedTowers.CreateTowerStates();
                }
            }
            else
            {
                Debug.Log("There isn't a disk data.");
                playerData = new GameplayData();
                playerData.OwnedCurrencies.IncreaseCurrency(CurrencyType.Coin, 0);
                playerData.OwnedTowers.CreateTowerStates();
            }

            _gameplayData = playerData;
        }
        
        public static TowerType GetRandomTowerType()
        {
            Array values = Enum.GetValues(typeof(TowerType));
            TowerType randomType = (TowerType)values.GetValue(Random.Range(0, values.Length));
            return randomType;
        }
    }
}