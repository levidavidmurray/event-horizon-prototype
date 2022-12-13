using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    [SerializeField] private int _HoverSortingOrder = 10;
    [SerializeField] private float _SelectRotTime = 0.2f;
    
    public Card m_Card;
    public Action m_OnMouseDown;
    public Action m_OnMouseUp;

    private SpriteRenderer _SR;
    private Sprite _CardBackSprite;
    private bool _IsFaceDown;

    private int _DefaultSortingOrder = -1;
    private Vector3 _RotBeforeSelect;

    private int _RotTweenId;
    private bool _IsOnSlot;
    
    public int SortingOrder
    {
        get => _SR.sortingOrder;
        set
        {
            _SR.sortingOrder = value;
            if (_DefaultSortingOrder < 0)
                _DefaultSortingOrder = _SR.sortingOrder;
        }
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

    public void SetOnSlot()
    {
        _IsOnSlot = true;
    }

    private void OnMouseEnter()
    {
        SortingOrder = _HoverSortingOrder;
    }

    private void OnMouseExit()
    {
        SortingOrder = _DefaultSortingOrder;
    }

    private void OnMouseDown()
    {
        print("OnMouseDown");
        
        if (_RotBeforeSelect.magnitude == 0f)
        {
            _RotBeforeSelect = transform.rotation.eulerAngles;
        }

        _IsOnSlot = false;
        
        m_OnMouseDown?.Invoke();

        LeanTween.cancel(_RotTweenId);
        _RotTweenId = transform.LeanRotate(Vector3.zero, _SelectRotTime).id;
    }
    
    private void OnMouseUp()
    {
        print("OnMouseUp");
        m_OnMouseUp?.Invoke();

        if (_IsOnSlot) return;
        LeanTween.cancel(_RotTweenId);
        _RotTweenId = transform.LeanRotate(_RotBeforeSelect, _SelectRotTime).id;
    }
}