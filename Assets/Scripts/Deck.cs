using System;
using UnityEngine;

namespace DefaultNamespace
{
    
    public class Deck : MonoBehaviour
    {
        public DeckSlot[] m_DeckSlots { get; private set; }

        private void Awake()
        {
            m_DeckSlots = GetComponentsInChildren<DeckSlot>();
        }
        
        public void AddCardToSlot(Card card, int slotIndex)
        {
            m_DeckSlots[slotIndex].AddCard(card);
        }
        
        public void RemoveCardFromSlot(int slotIndex)
        {
            m_DeckSlots[slotIndex].RemoveCard();
        }

        public Card GetCardFromSlot(int slotIndex)
        {
            return m_DeckSlots[slotIndex].m_Card;
        }
        
    }
}