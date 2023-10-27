using UnityEngine;

namespace NecatiAkpinar.Data
{
    public abstract class BaseProjectileData : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;

        public float MovementSpeed => _movementSpeed;
    }
}