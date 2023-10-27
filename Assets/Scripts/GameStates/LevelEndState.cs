using System;
using NecatiAkpinar.Data;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NecatiAkpinar.GameStates
{
    public class LevelEndState : BaseGameState
    {
        private Action<GameStateType, GameStateInfoTransporter> _changeStateCallback;

        public LevelEndState(Action<GameStateType, GameStateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
        }

        public override void Start(GameStateInfoTransporter stateInfoTransporter)
        {
            EventManager.OnLevelEndWindowActivated?.Invoke(stateInfoTransporter.IsLevelWin);
        }

        public override void End()
        {
        }
    }
}