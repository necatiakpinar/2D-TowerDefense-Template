using NecatiAkpinar.Data;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace NecatiAkpinar.Editor.LevelEditor
{
    /// <summary>
    ///   <para> Level editor controls the general logic of the levels 
    /// </summary>
    public class LevelEditor : EditorWindow
    {
        private readonly string _resetGameDataButton = "Reset Game Data";
        private readonly string _changeCurrentLevelIndexButton = "Update Level";
        private readonly string _changeKilledAmountButton = "Update Killed Enemy Amount";
        private readonly string _changedCoinCurrencyAmountButton = "Update Coin Currency";

        private int updatedCurrentLevelValue = 0;
        private int updatedKilledEnemyAmountValue = 0;
        private int updatedCoinCurrencyAmountValue = 0;

        private GUIStyle centeredStyle;

        [MenuItem("TowerDefenseGame/Level Editor")]
        public static void ShowWindow()
        {
            GetWindow<LevelEditor>("Level Editor");
        }

        private void OnGUI()
        {
            centeredStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold
            };

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Reset", centeredStyle, GUILayout.ExpandWidth(true));
            if (GUILayout.Button(_resetGameDataButton))
                ResetGameData();

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Level Settings", centeredStyle, GUILayout.ExpandWidth(true));

            GUILayout.BeginHorizontal();
            updatedCurrentLevelValue = EditorGUILayout.IntField("Update Current Level = ", updatedCurrentLevelValue);
            if (GUILayout.Button(_changeCurrentLevelIndexButton))
                UpdateCurrentLevel();

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Currency Settings", centeredStyle, GUILayout.ExpandWidth(true));

            GUILayout.BeginVertical();
            updatedCoinCurrencyAmountValue = EditorGUILayout.IntField("Update Coin Currency Amount = ", updatedCoinCurrencyAmountValue);
            if (GUILayout.Button(_changedCoinCurrencyAmountButton))
                UpdateCoinCurrencyAmount();

            GUILayout.Space(10);
            
            updatedKilledEnemyAmountValue = EditorGUILayout.IntField("Update Killed Enemy Amount = ", updatedKilledEnemyAmountValue);
            if (GUILayout.Button(_changeKilledAmountButton))
                UpdateEnemyKilledAmount();

            GUILayout.EndVertical();

            GUILayout.EndVertical();
        }

        private void ResetGameData()
        {
            Player.ResetData();
        }

        private void UpdateCurrentLevel()
        {
            Player.LoadSaveDataFromDisk();
            Player.GameplayData.ChangeCurrentLevelIndex(updatedCurrentLevelValue);
            Player.SaveDataToDisk();
        }

        private void UpdateEnemyKilledAmount()
        {
            Player.LoadSaveDataFromDisk();
            Player.GameplayData.ChangeTotalKilledEnemyAmount(updatedKilledEnemyAmountValue);
            Player.SaveDataToDisk();
        }

        private void UpdateCoinCurrencyAmount()
        {
            Player.LoadSaveDataFromDisk();
            Player.GameplayData.ChangeCoinCurrencyAmount(updatedCoinCurrencyAmountValue);
            Player.SaveDataToDisk();
        }
    }
}