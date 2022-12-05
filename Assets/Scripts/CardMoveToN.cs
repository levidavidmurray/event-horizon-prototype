using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New CardMoveToN", menuName = "Custom/New CardMoveToN", order = 0)]
    public class CardMoveToN : Card
    {
        [SerializeField] private int _SpaceIndex;
        
        public override int Activate(PlayerToken token)
        {
            Debug.Log($"[CardMoveToN]", this);
            return _SpaceIndex - token.CurrentSpaceIndex;
        }
    }
}