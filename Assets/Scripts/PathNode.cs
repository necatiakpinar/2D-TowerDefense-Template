using System;
using UnityEngine;

namespace NecatiAkpinar.Utils
{
    public class PathNode : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            DisableVisuals();
        }

        private void DisableVisuals()
        {
            _spriteRenderer.enabled = false;
        }
    }
}