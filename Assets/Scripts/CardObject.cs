using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public Card m_Card;

    private SpriteRenderer _SR;

    private void Awake()
    {
        _SR = transform.Find("Card_Back").GetComponent<SpriteRenderer>();
    }

    public int Activate(PlayerToken token)
    {
        _SR.sprite = m_Card.cardSprite;
        return m_Card.Activate(token);
    }
}
