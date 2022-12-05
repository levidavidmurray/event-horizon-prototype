using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerToken : MonoBehaviour
    {
        private Space _Space;
        private Vector3 _StartPos;

        public int CurrentSpaceIndex => _Space?.m_Index ?? -1;

        private void Start()
        {
            _StartPos = transform.position;
        }

        // Return index of new space
        public Card SetTokenSpace(Space space)
        {
            _Space = space;
            
            if (!_Space)
            {
                transform.position = _StartPos;
                return null;
            }
            
            transform.position = space.transform.position;
            
            return space.m_DeckSlot.m_Card;
        }
        
    }
}