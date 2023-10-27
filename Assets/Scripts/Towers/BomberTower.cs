using System;
using System.Collections;
using NecatiAkpinar.Abstracts;
using UnityEngine;

namespace NecatiAkpinar.Towers
{
    public class BomberTower : BaseTower
    {
        [SerializeField] private Transform _weaponHandler;
        [SerializeField] private Transform _projectileSpawnTransform;
        
        void Update()
        {
            if(_targetEnemy != null)
            {
                Vector2 directionToTarget = _targetEnemy.transform.position - _weaponHandler.transform.position;
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                _weaponHandler.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        protected override IEnumerator Shoot()
        {
            yield return base.Shoot();

            _targetEnemy = GetFirstTargetEnemy();
            if (_targetEnemy == null)
                yield break;
            
            BaseProjectile projectile = Instantiate(_projectilePF, null);
            projectile.transform.position = _projectileSpawnTransform.position;
            projectile.Launch(_targetEnemy.transform.position, _statsData.DamageAmount);
            yield return _waitForNextShot;
        }
    }
}