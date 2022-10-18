using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBoard : MonoBehaviour
    {
        [SerializeField] private PlayerToken _ActivePlayerToken;
        [SerializeField] private Card[] _Cards;
        [SerializeField] private Deck _Deck;

        public void Awake()
        {
            for (int i = 0; i < _Cards.Length; i++)
            {
                if (!_Cards[i]) continue;
                
                _Deck.AddCardToSlot(_Cards[i], i);
            }
        }

        public void RollDice()
        {
            
        }
        
    }
}