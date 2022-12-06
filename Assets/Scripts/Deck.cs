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
        
        public void AddCardToSlot(CardObject card, int slotIndex)
        {
            m_DeckSlots[slotIndex].SetCard(card);
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