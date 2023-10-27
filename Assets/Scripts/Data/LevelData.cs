using UnityEngine;

namespace NecatiAkpinar.Data
{
    [CreateAssetMenu(fileName = "LevelData_", menuName = "Data/Level/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _targetEnemyCount = 10;
        [SerializeField] private float _enemySpawnSpeed = 0.25f;

        public int TargetEnemyCount => _targetEnemyCount;
        public float EnemySpawnSpeed => _enemySpawnSpeed;
    }
}