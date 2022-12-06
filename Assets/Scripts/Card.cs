using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Custom/New Card", order = 0)]
    public abstract class Card : ScriptableObject
    {
        public int totalInDeck = 0;
        // Return number of spaces to move
        public abstract int Activate(PlayerToken token);
    }
}