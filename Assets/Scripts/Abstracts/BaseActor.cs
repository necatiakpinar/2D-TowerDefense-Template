using System;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Managers;
using UnityEngine;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BaseActor : MonoBehaviour
    {
        public virtual void ResetActor()
        {
            gameObject.SetActive(false);
        }
    }
}