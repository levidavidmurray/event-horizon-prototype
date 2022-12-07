using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerToken : MonoBehaviour
    {
        [SerializeField] private float _JumpCurveHandleHeight = 1.5f;
        [SerializeField] private float _JumpCurveTime = 0.5f;
        [SerializeField] private float _DebugPathPointsRadius = 0.5f;
        
        private Space _Space;
        private Vector3 _StartPos;

        private LTBezierPath _VisualizePath;
        private Vector3[] _PathPoints;

        private Action[] _JumpActions;
        private int _JumpActionIndex = -1;
        private bool _IsJumping;
        private Action _OnJumpActionsComplete;

        public int CurrentSpaceIndex => _Space?.m_Index ?? -1;

        private void Start()
        {
            _StartPos = transform.position;
        }

        private void Update()
        {
            if (_JumpActions != null && !_IsJumping)
            {
                _IsJumping = true;
                _JumpActions[_JumpActionIndex]?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if(_VisualizePath != null)
                _VisualizePath.gizmoDraw(); // To Visualize the path, use this method
            if (_PathPoints != null)
            {
                foreach (var pos in _PathPoints)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(pos, _DebugPathPointsRadius);
                }
            }
        }

        public void JumpToPositions(Vector3[] positions, Action onComplete)
        {
            print($"Jumping {positions.Length} spaces");
            _JumpActionIndex = 0;
            _JumpActions = new Action[positions.Length];
            _OnJumpActionsComplete = onComplete;
            for (var i = 0; i < positions.Length; i++)
            {
                var pos = positions[i];
                var j = i;
                _JumpActions[i] = () =>
                {
                    JumpToPosition(pos, () =>
                    {
                        print($"Jumped space {j}");
                        _JumpActionIndex++;
                        _IsJumping = false;

                        // All jumps complete
                        if (_JumpActionIndex >= _JumpActions.Length)
                        {
                            _JumpActions = null;
                            _OnJumpActionsComplete?.Invoke();
                        }
                    });
                };
            }

            // _IsJumping = true;
        }

        public void JumpToPosition(Vector3 endPos, Action onComplete)
        {
            var curPos = transform.position;
            print($"Jumping from {curPos} to {endPos}");
            float halfX = endPos.x - curPos.x;
            // var handlePos = new Vector3(halfX, curPos.y + _JumpCurveHandleHeight, curPos.z);
            var handle1Pos = new Vector3(endPos.x, curPos.y + _JumpCurveHandleHeight, curPos.z);
            var handle2Pos = new Vector3(curPos.x, curPos.y + _JumpCurveHandleHeight, curPos.z);

            var path = new[] { curPos, handle1Pos, handle2Pos, endPos };
            _VisualizePath = new LTBezierPath(path);
            _PathPoints = path;

            transform.LeanMove(path, _JumpCurveTime).setOnComplete(onComplete);
        }

        // Return index of new space
        public CardObject SetTokenSpace(Space space)
        {
            _Space = space;
            
            if (!_Space)
            {
                transform.position = _StartPos;
                return null;
            }
            
            transform.position = space.transform.position;

            if (!_Space.m_DeckSlot) return null;

            return space.m_DeckSlot.RemoveCard();
        }
        
    }
}