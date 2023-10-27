using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Misc;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Data;
using NecatiAkpinar.GameStates;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public class GameManager : MonoBehaviour
    {
        private StartingState _startingState;
        private InGameState _inGameState;
        private LevelEndState _levelEndState;

        private BaseGameState _currentState;

        private void Awake()
        {
            Player.LoadSaveDataFromDisk();
        }

        private void Start()
        {
            _startingState = new StartingState(ChangeGameState);
            _inGameState = new InGameState(ChangeGameState);
            _levelEndState = new LevelEndState(ChangeGameState);

            GameStateInfoTransporter infoTransporter = new GameStateInfoTransporter();
            ChangeGameState(GameStateType.Starting, infoTransporter);
        }

        private void ChangeGameState(GameStateType stateType, GameStateInfoTransporter _infoTransporter)
        {
            switch (stateType)
            {
                case GameStateType.Starting:
                    _currentState = _startingState;
                    break;
                case GameStateType.InGame:
                    _currentState = _inGameState;
                    break;
                case GameStateType.LevelEnd:
                    _currentState = _levelEndState;
                    break;
            }

            _currentState.Start(_infoTransporter);
        }

        private void OnApplicationQuit()
        {
            Player.SaveDataToDisk();
        }
    }
}