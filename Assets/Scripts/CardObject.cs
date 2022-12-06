using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    [SerializeField] private Card _Card;

    public Card m_Card => _Card;
}
