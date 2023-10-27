using DG.Tweening;
using NecatiAkpinar.Abstracts;
using UnityEngine;

namespace NecatiAkpinar.Projectiles
{
    public class LinearProjectile : BaseProjectile
    {
        public override void Launch(Vector3 target, int damageAmount)
        {
            base.Launch(target, damageAmount);

            transform.DOMove(target, _data.MovementSpeed).OnComplete(() =>
            {
                ToggleCollider(true);
            });
        }
    }
}