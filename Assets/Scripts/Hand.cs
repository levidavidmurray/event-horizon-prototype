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

        private int _DefaultSortingLayer = -1;

        private void Awake()
        {
            _CardObjects = new CardObject[_HandSize];
        }

        public void SetCardInHand(Card card)
        {
            var noCardIndex = GetMissingCardIndex();
            
            // Hand is full
            if (noCardIndex < 0) return;
            
            
            CardObject cardObj = Instantiate(_CardObj, transform.position, Quaternion.identity)
                .GetComponent<CardObject>();

            if (_DefaultSortingLayer < 0)
            {
                _DefaultSortingLayer = cardObj.SortingOrder;
            }

            var pos = cardObj.transform.position;
            pos.x += (noCardIndex - 1) * _CardSpacing.x;
            if (noCardIndex != 1)
            {
                pos.y -= _CardSpacing.y;
                var rotDir = noCardIndex == 0 ? 1 : -1;
                cardObj.transform.Rotate(0, 0, rotDir * _CardRot);
            }
            cardObj.transform.position = pos;

            cardObj.m_Card = card;
            cardObj.FaceUp();

            _CardObjects[noCardIndex] = cardObj;
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