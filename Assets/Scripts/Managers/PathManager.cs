using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Utils;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] PathNode[] _pathNodes;

        public PathNode[] PathNodes => _pathNodes;

        private void OnEnable()
        {
            EventManager.GetPathNodes += () => { return _pathNodes; };
        }

        private void OnDisable()
        {
            EventManager.GetPathNodes -= () => { return _pathNodes; };   
        }
        
    }
}