using NecatiAkpinar.Abstracts;
using UnityEngine;

namespace NecatiAkpinar.Utils
{
    public class TowerDestroyer : MonoBehaviour
    {
        public void DestroyTower(BaseTower towerToDelete)
        {
            towerToDelete.ReturnToPool();
        }
    }
}