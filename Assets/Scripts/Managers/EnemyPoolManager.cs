using System;
using System.Collections.Generic;
using NecatiAkpinar.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Managers
{
    [System.Serializable]
    public class EnemyPoolObject
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private BaseEnemy _enemyPF;
        [SerializeField] private int _size;

        public EnemyType EnemyType => _enemyType;
        public BaseEnemy EnemyPF => _enemyPF;
        public int Size => _size;
    }

    public class EnemyPoolManager : MonoBehaviour
    {
        [SerializeField] private List<EnemyPoolObject> _pools;
        [SerializeField] private Transform _enemyParent;

        private Dictionary<EnemyType, Queue<BaseEnemy>> _poolDictionary;

        public static EnemyPoolManager Instance;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _poolDictionary = new Dictionary<EnemyType, Queue<BaseEnemy>>();

            foreach (var pool in _pools)
            {
                Queue<BaseEnemy> objectPool = new Queue<BaseEnemy>();

                for (int i = 0; i < pool.Size; i++)
                {
                    BaseEnemy enemy = Instantiate(pool.EnemyPF, _enemyParent);
                    enemy.gameObject.SetActive(false);
                    objectPool.Enqueue(enemy);
                }

                _poolDictionary.Add(pool.EnemyType, objectPool);
            }
        }

        public BaseEnemy SpawnFromPool(EnemyType enemyType, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(enemyType) || _poolDictionary[enemyType].Count == 0)
                return null;

            BaseEnemy enemyToSpawn = _poolDictionary[enemyType].Dequeue();
            enemyToSpawn.gameObject.SetActive(true);
            enemyToSpawn.transform.localPosition = position;
            enemyToSpawn.transform.rotation = rotation;
            
            enemyToSpawn.Init();

            return enemyToSpawn;
        }

        public void ReturnToPool(EnemyType _objectType, BaseEnemy objectToReturn)
        {
            if (!_poolDictionary.ContainsKey(_objectType))
                return;

            objectToReturn.ResetActor();
            _poolDictionary[_objectType].Enqueue(objectToReturn);
        }
    }
}