using System;
using NecatiAkpinar.Managers;
using UnityEngine;

namespace NecatiAkpinar.Utils
{
    public class PathVisualizer : MonoBehaviour
    {
        [SerializeField] private Color lineColor = Color.green;

        private PathManager _pathManager;
        private Transform[] _pathNodes;

        private void OnValidate()
        {
            _pathManager = GetComponent<PathManager>();
        }

        void OnDrawGizmos()
        {
            if (_pathManager == null || _pathManager.PathNodes.Length < 2)
                return;

            Gizmos.color = lineColor;
            for (int i = 0; i < _pathManager.PathNodes.Length - 1; i++)
                if (_pathManager.PathNodes[i] != null && _pathManager.PathNodes[i + 1] != null)
                    Gizmos.DrawLine(_pathManager.PathNodes[i].transform.position, _pathManager.PathNodes[i + 1].transform.position);
        }
    }
}