using System;
using NecatiAkpinar.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NecatiAkpinar.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Buttons")] [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadGameButton;

        private void Start()
        {
            _newGameButton.onClick.AddListener(StartNewGame);
            _loadGameButton.onClick.AddListener(LoadGame);
        }

        private void StartNewGame()
        {
            Player.ResetData();
            SceneManager.LoadScene("GameplayScene");
        }

        private void LoadGame()
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
}