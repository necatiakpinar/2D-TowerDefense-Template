using System;
using NecatiAkpinar.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Misc
{
    public class GameReferences : MonoBehaviour
    {
        [SerializeField] private EnemyCurrencyProvider _enemyCurrencyProvider;

        public EnemyCurrencyProvider EnemyCurrencyProvider => _enemyCurrencyProvider;

        public static GameReferences Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }
    }
}