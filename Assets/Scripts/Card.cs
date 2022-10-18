using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Custom/New Card", order = 0)]
    public abstract class Card : ScriptableObject
    {
        public abstract void Activate(PlayerToken token);
    }
}