using System.Collections;
using DG.Tweening;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Managers;
using TMPro;
using UnityEngine;

namespace NecatiAkpinar.VFXs
{
    public class DamageTextVFX : BaseVFX
    {
        private TMP_Text _damageLabel;

        private readonly float _tweenDuration = 1.0f;
        private readonly float _textTargetYOffset = 1.0f;

        public override void Init()
        {
            _damageLabel = GetComponent<TextMeshPro>();
        }

        public void Play(int damageAmount)
        {
            _damageLabel.text = damageAmount.ToString();
            transform.DOMoveY(transform.position.y + _textTargetYOffset, _tweenDuration).OnComplete(ReturnToPool);
        }

        public override void ReturnToPool()
        {
            transform.DOKill(true);
            VFXPoolManager.Instance.ReturnToPool(_vfxType, this);
        }
    }
}