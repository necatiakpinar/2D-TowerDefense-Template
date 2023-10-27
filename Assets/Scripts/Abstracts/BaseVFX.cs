using UnityEngine;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BaseVFX : MonoBehaviour
    {
        [SerializeField] protected VFXType _vfxType;
        public abstract void Init();

        public virtual void ResetVFX()
        {
            gameObject.SetActive(false);
        }

        public abstract void ReturnToPool();

    }
}