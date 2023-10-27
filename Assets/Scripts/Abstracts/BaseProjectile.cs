using System;
using NecatiAkpinar.Data;
using NecatiAkpinar.Enemies;
using NecatiAkpinar.Managers;
using NecatiAkpinar.VFXs;
using UnityEngine;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected BaseProjectileData _data;
        private Collider2D _collider;
        private int _damageAmount;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            ToggleCollider(false);
        }

        public virtual void Launch(Vector3 target, int damageAmount)
        {
            _damageAmount = damageAmount;
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            BaseEnemy enemy = col.GetComponent<BaseEnemy>();

            if (enemy)
            {
                enemy.TakeDamage(_damageAmount);
                var vfx = VFXPoolManager.Instance.SpawnFromPool<DamageTextVFX>(VFXType.DamageText, enemy.transform.position, Quaternion.identity);
                if (vfx)
                    vfx.Play(_damageAmount);
                Destroy(this.gameObject);
            }
        }

        protected void ToggleCollider(bool isActive)
        {
            _collider.enabled = isActive;
        }
    }
}