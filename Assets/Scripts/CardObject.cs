using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public Card m_Card;

    private SpriteRenderer _SR;
    private Sprite _CardBackSprite;
    private bool _IsFaceDown;

    public int SortingOrder
    {
        get => _SR.sortingOrder;
        set => _SR.sortingOrder = value;
    }

    private void Awake()
    {
        _SR = transform.Find("Card_Back").GetComponent<SpriteRenderer>();
        _CardBackSprite = _SR.sprite;
    }

    public void FaceDown()
    {
        _SR.sprite = _CardBackSprite;
    }

    public void FaceUp()
    {
        _SR.sprite = m_Card.cardSprite;
    }

    public int Activate(PlayerToken token)
    {
        FaceUp();
        return m_Card.Activate(token);
    }

    private void OnMouseDown()
    {
        print("OnMouseDown");
    }
    
    private void OnMouseUp()
    {
        print("OnMouseUp");
    }
}
