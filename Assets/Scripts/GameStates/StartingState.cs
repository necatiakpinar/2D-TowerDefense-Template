using System;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Data;
using NecatiAkpinar.Managers;
using UnityEngine;

namespace NecatiAkpinar.GameStates
{
    public class StartingState : BaseGameState
    {
        private Action<GameStateType, GameStateInfoTransporter> _changeStateCallback;

        private readonly int _givenTowerAmountFTUE = 2;

        public StartingState(Action<GameStateType, GameStateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
        }

        public override void Start(GameStateInfoTransporter stateInfoTransporter)
        {
            GameplayData gameplayData = Player.GameplayData;
            if (!gameplayData.OwnedTowers.HasPurchasedTower() && gameplayData.CurrentLevelIndex == 0)
            {
                TowerType randomTowerType = Player.GetRandomTowerType();
                EventManager.OnTowerPurchased?.Invoke(randomTowerType, true);
            }

            End();
        }

        public override void End()
        {
            GameStateInfoTransporter infoTransporter = new GameStateInfoTransporter();
            _changeStateCallback?.Invoke(GameStateType.InGame, infoTransporter);
        }
    }
}