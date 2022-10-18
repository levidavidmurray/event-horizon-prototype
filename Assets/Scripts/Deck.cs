using UnityEngine;

namespace DefaultNamespace
{
    
    public class Deck : MonoBehaviour
    {
        [SerializeField] private DeckSlot[] _DeckSlot;

        public void AddCardToSlot(Card card, int slotIndex)
        {
            _DeckSlot[slotIndex].AddCard(card);
        }
        
        public void RemoveCardFromSlot(int slotIndex)
        {
            _DeckSlot[slotIndex].RemoveCard();
        }
        
    }
}