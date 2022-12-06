using UnityEngine;

namespace DefaultNamespace
{
    public class DeckSlot : MonoBehaviour
    {
        private CardObject _CardObj;

        public Card m_Card => _CardObj?.m_Card;

        public void SetCard(CardObject card)
        {
            _CardObj = card;
        }

        public CardObject RemoveCard()
        {
            var cardObj = _CardObj;
            _CardObj = null;
            return cardObj;
        }
        
    }
}