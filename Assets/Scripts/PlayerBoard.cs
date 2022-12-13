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
        [SerializeField] private Hand _Hand;
        [SerializeField] private Deck _Deck;
        [SerializeField] private CardSpawner _CardSpawner;
        [SerializeField] private GameObject _CardObj;
        [SerializeField] private float _DestroyCardDelay = 1f;

        private Space[] _Spaces;

        public void Start()
        {
            _Spaces = GetComponentsInChildren<Space>();
            
            // Correlate spaces with deck slots
            for (var i = 0; i < _Spaces.Length; i++)
            {
                _Spaces[i].SetIndex(i);
                
                // First and last space should not have deck slots
                if (i == 0 || i == _Spaces.Length - 1) continue;
                
                _Spaces[i].SetDeckSlot(_Deck.m_DeckSlots[i-1]);
            }

            _ActivePlayerToken.SetTokenSpace(_Spaces[0]);

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

            _Hand.SetCardInHand(_CardSpawner.DrawCard());
            _Hand.SetCardInHand(_CardSpawner.DrawCard());
            _Hand.SetCardInHand(_CardSpawner.DrawCard());
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

            if (_Hand.SelectedCardObj)
            {
                float closestDist = Mathf.Infinity;
                int closestDistIndex = -1;
                // calculate closest deck slot to card
                for (int i = 0; i < _Deck.m_DeckSlots.Length; i++)
                {
                    var dist = Vector2.Distance(
                        _Deck.m_DeckSlots[i].transform.position,
                        _Hand.SelectedCardObj.transform.position
                    );

                    if (dist > closestDist || _Deck.m_DeckSlots[i].m_Card) continue;

                    closestDist = dist;
                    closestDistIndex = i;
                }

                _Hand.m_ClosestDeckSlot = _Deck.m_DeckSlots[closestDistIndex];
            }
        }

        public void RollDice()
        {
            if (_ActivePlayerToken.IsJumping) return;
            
            int stepsToMove = Random.Range(1, 4);
            // int stepsToMove = Random.Range(2, 3);
            print($"ROLL {stepsToMove}");
            int newSpaceIndex = _ActivePlayerToken.CurrentSpaceIndex + stepsToMove;
            
            UpdateTokenSpace(newSpaceIndex);
        }

        private void UpdateTokenSpace(int newSpaceIndex)
        {
            if (newSpaceIndex > _Spaces.Length - 1 || newSpaceIndex < 0)
            {
                print($"OVER!");
                return;
            }

            int curSpaceIndex = _ActivePlayerToken.CurrentSpaceIndex;
            int spacesToMove = newSpaceIndex - curSpaceIndex;
            int dir = spacesToMove < 0 ? -1 : 1;
            var spacePositions = new Vector3[Mathf.Abs(spacesToMove)];
            
            for (var i = 0; i < Mathf.Abs(spacesToMove); i++)
            {
                // find space in direction
                var spaceIndex = curSpaceIndex + dir * (i + 1);
                var space = _Spaces[spaceIndex];
                spacePositions[i] = space.transform.position;
            }
            
            _ActivePlayerToken.JumpToPositions(spacePositions, () =>
            {
                CardObject cardObj = _ActivePlayerToken.SetTokenSpace(_Spaces[newSpaceIndex]);

                if (!cardObj) return;

                print($"Activate: {cardObj.m_Card}");

                int numSpacesToMove = cardObj.Activate(_ActivePlayerToken);

                LeanTween.delayedCall(_DestroyCardDelay, () =>
                {
                    Destroy(cardObj.gameObject);
                });

                // Destroy(cardObj.gameObject);

                newSpaceIndex = Mathf.Max(newSpaceIndex + numSpacesToMove, 0);

                if (numSpacesToMove != 0)
                {
                    UpdateTokenSpace(newSpaceIndex);
                }
            });
        }
        
    }
}