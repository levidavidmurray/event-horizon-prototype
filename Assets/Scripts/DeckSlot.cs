using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeckSlot : MonoBehaviour
    {
        private CardObject _CardObj;
        private Action _RemoveCardAction;
        public Card m_Card => _CardObj?.m_Card;

        public void SetCard(CardObject card)
        {
            _CardObj = card;
            _RemoveCardAction = () =>
            {
                _CardObj.m_OnMouseDown -= _RemoveCardAction;
                RemoveCard();
            };
            _CardObj.m_OnMouseDown += _RemoveCardAction;
            _CardObj.SetOnSlot();
        }

        public CardObject RemoveCard()
        {
            var cardObj = _CardObj;
            _CardObj = null;
            return cardObj;
        }
        
    }
}