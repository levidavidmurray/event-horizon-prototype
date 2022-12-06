using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class PlayerBoard : MonoBehaviour
    {
        [SerializeField] private PlayerToken _ActivePlayerToken;
        [SerializeField] private Card[] _Cards;
        [SerializeField] private Deck _Deck;
        [SerializeField] private CardSpawner _CardSpawner;
        [SerializeField] private GameObject _CardObj;

        private Space[] _Spaces;

        public void Start()
        {
            _Spaces = GetComponentsInChildren<Space>();
            for (var i = 0; i < _Spaces.Length; i++)
            {
                _Spaces[i].SetIndex(i);
                _Spaces[i].SetDeckSlot(_Deck.m_DeckSlots[i]);
            }

            // Debug with hardcoded cards
            if (_Cards.Length > 0)
            {
                for (var i = 0; i < _Cards.Length; i++)
                {
                    if (!_Cards[i]) continue;
                    var deckSlot = _Deck.m_DeckSlots[i];
                    var cardObj = Instantiate(_CardObj, deckSlot.transform.position, Quaternion.identity).GetComponent<CardObject>();
                    cardObj.m_Card = _Cards[i];
                    deckSlot.SetCard(cardObj);
                }

                return;
            }

            // return;
            _Cards = new Card[3];
            
            for (int i = 0; i < _Cards.Length; i++)
            {
                int slotIndex = Random.Range(0, _Spaces.Length);
                while (_Deck.GetCardFromSlot(slotIndex) != null)
                {
                    slotIndex = Random.Range(0, _Spaces.Length);
                }
                
                var deckSlot = _Deck.m_DeckSlots[slotIndex];
                var cardObj = Instantiate(_CardObj, deckSlot.transform.position, Quaternion.identity).GetComponent<CardObject>();
                cardObj.m_Card = _CardSpawner.DrawCard();
                
                deckSlot.SetCard(cardObj);
                // _Cards[i] = card;
            }
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                RollDice();
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public void RollDice()
        {
            int stepsToMove = Random.Range(1, 4);
            // int stepsToMove = Random.Range(2, 3);
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

            int curSpaceIndex = _ActivePlayerToken.CurrentSpaceIndex;
            int spacesToMove = newSpaceIndex - curSpaceIndex;
            int dir = spacesToMove < 0 ? -1 : 1;
            //
            // var moveActions = new Action[spacesToMove];
            //
            // // Start from end space and work backwards
            var spacePositions = new Vector3[Mathf.Abs(spacesToMove)];
            // var i = 0;
            // for (var spaceIndex = curSpaceIndex + dir; spaceIndex != newSpaceIndex; spaceIndex += dir)
            for (var i = 0; i < Mathf.Abs(spacesToMove); i++)
            {
                // find space in direction
                var space = _Spaces[curSpaceIndex + dir * (i+1)];
                spacePositions[i] = space.transform.position;
            }
            // var newSpace = _Spaces[newSpaceIndex];
            
            _ActivePlayerToken.JumpToPositions(spacePositions, () =>
            {

                CardObject cardObj = _ActivePlayerToken.SetTokenSpace(_Spaces[newSpaceIndex]);

                if (!cardObj) return;

                Card card = cardObj.m_Card;

                int numSpacesToMove = card.Activate(_ActivePlayerToken);
                
                Destroy(cardObj.gameObject);

                if (numSpacesToMove != 0)
                {
                    UpdateTokenSpace(newSpaceIndex + numSpacesToMove);
                }
            });
        }
        
    }
}