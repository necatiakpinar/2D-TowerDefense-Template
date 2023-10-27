using System;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    [System.Serializable]
    public class TowerPoolObject
    {
        [SerializeField] private TowerType _towerType;
        [SerializeField] private BaseTower _towerPF;
        [SerializeField] private int _size;

        public TowerType TowerType => _towerType;
        public BaseTower TowerPF => _towerPF;
        public int Size => _size;
    }

    public class TowerPoolManager : MonoBehaviour
    {
        [SerializeField] private List<TowerPoolObject> _pools;
        [SerializeField] private Transform _towerParent;

        private Dictionary<TowerType, Queue<BaseTower>> _poolDictionary;

        public static TowerPoolManager Instance;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            
            Init();
        }

        private void Init()
        {
            _poolDictionary = new Dictionary<TowerType, Queue<BaseTower>>();

            foreach (var pool in _pools)
            {
                Queue<BaseTower> objectPool = new Queue<BaseTower>();

                for (int i = 0; i < pool.Size; i++)
                {
                    BaseTower tower = Instantiate(pool.TowerPF, _towerParent);
                    tower.gameObject.SetActive(false);
                    tower.name = $"Tower {i}";
                    objectPool.Enqueue(tower);
                }

                _poolDictionary.Add(pool.TowerType, objectPool);
            }
        }

        public BaseTower SpawnFromPool(TowerType towerType, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(towerType) || _poolDictionary[towerType].Count == 0)
                return null;

            BaseTower towerToSpawn = _poolDictionary[towerType].Dequeue();
            towerToSpawn.gameObject.SetActive(true);
            towerToSpawn.transform.localPosition = position;
            towerToSpawn.transform.rotation = rotation;
            
            //towerToSpawn.Init();

            return towerToSpawn;
        }

        public void ReturnToPool(TowerType towerType, BaseTower towerToReturn)
        {
            if (!_poolDictionary.ContainsKey(towerType))
                return;

            towerToReturn.ResetActor();
            towerToReturn.transform.parent = _towerParent;
            _poolDictionary[towerType].Enqueue(towerToReturn);
        }
    }
}