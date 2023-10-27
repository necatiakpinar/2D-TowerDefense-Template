using System;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Managers;
using UnityEngine;

namespace NecatiAkpinar.GameStates
{
    public class InGameState : BaseGameState
    {
        private Action<GameStateType, GameStateInfoTransporter> _changeStateCallback;

        public InGameState(Action<GameStateType, GameStateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
            SubscribeEvents();
        }

        public void SubscribeEvents()
        {
            EventManager.OnLevelFinished += SetLevelEndStatus;
        }

        public void UnsubscribeEvents()
        {
            EventManager.OnLevelFinished -= SetLevelEndStatus;
        }

        public override void Start(GameStateInfoTransporter stateInfoTransporter)
        {
        }

        public override void End()
        {
            UnsubscribeEvents();
        }

        public void SetLevelEndStatus(bool isLevelWin)
        {
            GameStateInfoTransporter stateInfoTranporter = new GameStateInfoTransporter(isLevelWin);
            _changeStateCallback.Invoke(GameStateType.LevelEnd, stateInfoTranporter);
        }
    }
}