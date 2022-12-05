using UnityEngine;

namespace DefaultNamespace
{
    public class Space : MonoBehaviour
    {
        public DeckSlot m_DeckSlot { get; private set; }
        public int m_Index { get; private set; }

        public void SetDeckSlot(DeckSlot slot)
        {
            m_DeckSlot = slot;
        }

        public void SetIndex(int i)
        {
            m_Index = i;
        }

    }
}