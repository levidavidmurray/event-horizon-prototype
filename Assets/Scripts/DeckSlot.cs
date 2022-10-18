using UnityEngine;

namespace DefaultNamespace
{
    public class DeckSlot : MonoBehaviour
    {
        
        private Card _Card;

        public Card m_Card => _Card;

        public void AddCard(Card card)
        {
            _Card = card;
        }

        public Card RemoveCard()
        {
            Card card = _Card;
            _Card = null;
            return card;
        }
        
    }
}