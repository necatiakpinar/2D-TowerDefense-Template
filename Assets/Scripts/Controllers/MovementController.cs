using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Utils;
using UnityEngine;

namespace NecatiAkpinar.Controllers
{
    public class MovementController
    {
        private BaseActor _actor;
        private PathNode[] _pathNodes;
        private int _currentPathNodeIndex;
        private float _movementDuration;
        private Ease _movementEase;
        private WaitForSeconds _waitDurationToReachTarget;
        private bool _canMove;
        
        public MovementController(BaseActor actor, PathNode[] pathNodes, float movementDuration, Ease movementEase)
        {
            _actor = actor;
            _pathNodes = pathNodes;
            _currentPathNodeIndex = 0;
            _movementDuration = movementDuration;
            _movementEase = movementEase;
            _waitDurationToReachTarget = new WaitForSeconds(_movementDuration);
            _canMove = true;
        }

        public void ActivateMovement()
        {
            if (_pathNodes.Length == 0 || _currentPathNodeIndex >= _pathNodes.Length)
                return;

            _actor.StartCoroutine(MoveTowardsToTargetPath());
        }

        public IEnumerator MoveTowardsToTargetPath()
        {
            PathNode pathNode = _pathNodes[_currentPathNodeIndex];
            _actor.transform.DOMove(pathNode.transform.position, _movementDuration).SetEase(_movementEase);
            yield return _waitDurationToReachTarget;

            _currentPathNodeIndex++;
            if (_currentPathNodeIndex >= _pathNodes.Length) //Make sure this event works one time!
            {
                EventManager.OnLevelFinished?.Invoke(false);
                yield break;
            }

            if (_canMove)
                _actor.StartCoroutine(MoveTowardsToTargetPath());

            yield break;
        }

        public void StopMovement()
        {
            _actor.transform.DOKill(false);
            _canMove = false;
        }
    }
}