using UnityEngine;

namespace DefaultNamespace
{
    public class Space : MonoBehaviour
    {
        [SerializeField] private DeckSlot _DeckSlot;
        [SerializeField] private int _Index;

        public int m_Index => _Index;
        public DeckSlot GetDeckSlot() => _DeckSlot;

    }
}