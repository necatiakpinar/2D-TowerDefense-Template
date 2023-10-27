using NecatiAkpinar.Abstracts;
using UnityEngine;

namespace NecatiAkpinar.Interfaces
{
    public interface ITowerPlacable
    {
        public void PlaceTower(BaseTower tower, ref ITowerPlacable towerPlacable);
        public void ReleaseTower();
        public BaseTower GetTower();
        public Transform GetTransform();
        public bool IsFull();

    }
}