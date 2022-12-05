using UnityEngine;

namespace DefaultNamespace
{

    public enum MoveDirection
    {
        Back = -1,
        Forward = 1,
    }
    
    [CreateAssetMenu(fileName = "New CardMoveForwardBackN", menuName = "Custom/New CardMoveForwardBackN", order = 0)]
    public class CardMoveForwardBackN : Card
    {
        [SerializeField] private MoveDirection _MoveDirection = MoveDirection.Forward;
        [SerializeField] private int _SpacesMoved = 1;
        
        public override int Activate(PlayerToken token)
        {
            Debug.Log($"[CardMoveForwardBackN] MoveDir: {_MoveDirection}, Spaces: {_SpacesMoved}");
            return (int)_MoveDirection * _SpacesMoved;
        }
    }
}