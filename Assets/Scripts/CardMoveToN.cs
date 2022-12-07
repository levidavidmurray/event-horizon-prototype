using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New CardMoveToN", menuName = "Custom/New CardMoveToN", order = 0)]
    public class CardMoveToN : Card
    {
        [SerializeField] private int _SpaceIndex;
        
        public override int Activate(PlayerToken token)
        {
            return _SpaceIndex - token.CurrentSpaceIndex;
        }

        public override string ToString()
        {
            return $"[Card::CardMoveToN] MoveTo{_SpaceIndex}";
        }
    }
}