using System;
using NecatiAkpinar.UI.Window;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public class UIManager: MonoBehaviour
    {
        [SerializeField] private GameplayWindow _gameplayWindow;
        [SerializeField] private LevelEndWindow _levelEndWindow;

        private void OnEnable()
        {
            EventManager.OnLevelEndWindowActivated += ActivateLevelEndWindow;
        }

        private void OnDisable()
        {
            EventManager.OnLevelEndWindowActivated -= ActivateLevelEndWindow;
        }

        private void Start()
        {
            _gameplayWindow.gameObject.SetActive(true);
            _levelEndWindow.gameObject.SetActive(false);
        }

        private void ActivateLevelEndWindow(bool isWin)
        {
            _gameplayWindow.gameObject.SetActive(false);
            _levelEndWindow.gameObject.SetActive(true);
            
            _levelEndWindow.Init(isWin);
        }
    }
}