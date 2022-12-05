using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class PlayerBoard : MonoBehaviour
    {
        [SerializeField] private PlayerToken _ActivePlayerToken;
        [SerializeField] private Card[] _Cards;
        [SerializeField] private Deck _Deck;

        private Space[] _Spaces;

        public void Start()
        {
            _Spaces = GetComponentsInChildren<Space>();
            for (var i = 0; i < _Spaces.Length; i++)
            {
                _Spaces[i].SetIndex(i);
                _Spaces[i].SetDeckSlot(_Deck.m_DeckSlots[i]);
            }
            
            for (int i = 0; i < _Cards.Length; i++)
            {
                if (!_Cards[i]) continue;
                
                _Deck.AddCardToSlot(_Cards[i], i);
            }
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                RollDice();
            }
        }

        public void MoveTokenForward()
        {
            UpdateTokenSpace(_ActivePlayerToken.CurrentSpaceIndex + 1);
        }

        public void MoveTokenBack()
        {
            UpdateTokenSpace(_ActivePlayerToken.CurrentSpaceIndex - 1);
        }

        public void RollDice()
        {
            int stepsToMove = Random.Range(1, 4);
            print($"ROLL {stepsToMove}");
            int newSpaceIndex = _ActivePlayerToken.CurrentSpaceIndex + stepsToMove;
            
            UpdateTokenSpace(newSpaceIndex);
        }

        private void UpdateTokenSpace(int newSpaceIndex)
        {
            if (newSpaceIndex > _Spaces.Length - 1)
            {
                print($"OVER!");
                return;
            }
            
            Card card = _ActivePlayerToken.SetTokenSpace(_Spaces[newSpaceIndex]);

            if (!card) return;

            int numSpacesToMove = card.Activate(_ActivePlayerToken);

            if (numSpacesToMove != 0)
            {
                UpdateTokenSpace(newSpaceIndex + numSpacesToMove);
            }
        }
        
    }
}