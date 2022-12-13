using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private GameObject _CardObj;
        [SerializeField] private int _HandSize = 3;
        [SerializeField] private Vector2 _CardSpacing = new(1.5f, 0.1f);
        [SerializeField] private float _CardRot = 2.5f;
        [SerializeField] private CardObject[] _CardObjects;
        [SerializeField] private int _CardMaxSortingOrder = 10;

        private int _DefaultSortingLayer = -1;
        private CardObject _SelectedCardObject;
        
        public DeckSlot m_ClosestDeckSlot;

        public CardObject SelectedCardObj => _SelectedCardObject;

        private void Awake()
        {
            _CardObjects = new CardObject[_HandSize];
        }

        private void Update()
        {
            if (_SelectedCardObject)
            {
                var curPos = _SelectedCardObject.transform.position;
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = curPos.z;
                _SelectedCardObject.transform.position = mousePos;
            }
        }

        public void SetCardInHand(Card card)
        {
            var cardIndex = GetMissingCardIndex();
            
            // Hand is full
            if (cardIndex < 0) return;
            
            CardObject cardObj = Instantiate(_CardObj, transform.position, Quaternion.identity)
                .GetComponent<CardObject>();

            cardObj.m_OnMouseDown += () => _SelectedCardObject = cardObj;
            cardObj.m_OnMouseUp += () =>
            {
                var cardDistToSlot = Vector2.Distance(
                    cardObj.transform.position,
                    m_ClosestDeckSlot.transform.position
                );
                
                var cardDistToHand = Vector2.Distance(
                    cardObj.transform.position,
                    transform.position
                );

                // Only set card to slot if the closest slot is closer than hand transform
                if (cardDistToSlot <= cardDistToHand)
                {
                    cardObj.transform.position = m_ClosestDeckSlot.transform.position;
                    m_ClosestDeckSlot.SetCard(cardObj);
                }
                else
                {
                    SetCardPosAndRotInHand(cardObj, cardIndex);
                }
                
                _SelectedCardObject = null;
            };

            if (_DefaultSortingLayer < 0)
            {
                _DefaultSortingLayer = cardObj.SortingOrder;
            }

            cardObj.m_Card = card;
            _CardObjects[cardIndex] = cardObj;
            SetCardPosAndRotInHand(cardObj, cardIndex);
        }

        private void SetCardPosAndRotInHand(CardObject cardObj, int cardIndex)
        {
            cardObj.SortingOrder = _DefaultSortingLayer + cardIndex;

            var cardTrans = cardObj.transform;
            var pos = transform.position;
            pos.x += (cardIndex - 1) * _CardSpacing.x;
            if (cardIndex != 1)
            {
                pos.y -= _CardSpacing.y;
                var rotDir = cardIndex == 0 ? 1 : -1;
                cardTrans.rotation = Quaternion.Euler(0, 0, rotDir * _CardRot);
            }
            cardTrans.position = pos;

            cardObj.FaceUp();
        }

        private int GetMissingCardIndex()
        {
            for (var i = 0; i < _CardObjects.Length; i++)
            {
                if (!_CardObjects[i]) return i;
            }

            return -1;
        }
    }
}