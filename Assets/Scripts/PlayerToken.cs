using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerToken : MonoBehaviour
    {
        private Space _Space;

        public void SetTokenSpace(Space space)
        {
            _Space = space;
            
            if (!space.GetDeckSlot().m_Card) return;
            
            space.GetDeckSlot().m_Card.Activate(this);
        }
    }
}