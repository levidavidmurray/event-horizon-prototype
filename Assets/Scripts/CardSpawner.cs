using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CardSpawner : MonoBehaviour
    {
        private Card[] _CardTypes;
        private int[] _CardTypeCount;
        private int[] _ShuffledCardIndices;
        private int _TopCardIndex = 0;

        private void Awake()
        {
            _CardTypes = Resources.LoadAll<Card>("Cards");
            
            
            _CardTypeCount = new int[_CardTypes.Length];

            int numCards = 0;
            for (var i = 0; i < _CardTypes.Length; i++)
            {
                numCards += _CardTypes[i].totalInDeck;
                _CardTypeCount[i] = _CardTypes[i].totalInDeck;
            }
            
            ShuffleCards(numCards);
        }

        public Card DrawCard()
        {
            int cardIndex = _ShuffledCardIndices[_TopCardIndex];
            _TopCardIndex++;
            return _CardTypes[cardIndex];
        }

        private void ShuffleCards(int numCards)
        {
            _ShuffledCardIndices = new int[numCards];
            for (var i = 0; i < numCards; i++)
            {
                int cardIndex = Random.Range(0, _CardTypes.Length);

                // Already shuffled in all of this type
                while (_CardTypeCount[cardIndex] <= 0)
                {
                    cardIndex = Random.Range(0, _CardTypes.Length);
                }

                _CardTypeCount[cardIndex]--;
                _ShuffledCardIndices[i] = cardIndex;
            }
        }
        
    }
}