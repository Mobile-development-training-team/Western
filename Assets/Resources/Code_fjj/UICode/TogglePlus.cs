using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TogglePlus : Toggle
{
    private Animator _animator;
    private Toggle _tog;

    public int ToggleID;
    public Image IconImage;

    public TogglePlusEvent OnTogPlusSelected = new TogglePlusEvent();

    protected override void Awake()
    {
        ToggleID = transform.GetSiblingIndex();
        _animator = GetComponent<Animator>();
        _tog = GetComponent<Toggle>();
        _tog.onValueChanged.AddListener(OnTogChanged);


    }

    public void Init(int ID)
    {
        ToggleID = ID;
    }

    private bool _pointIn = false;

    public override void OnPointerDown(PointerEventData eventData)
    {
        //_animator.Play("Pressed");
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_pointIn)
            isOn = true ;
        if (!isOn)
            _animator.Play("Normal");
        else
            _animator.Play("Pressed");
        //if (_pointIn)
        //{
        //    isOn = !isOn;
        //    if (isOn)
        //    {
        //        _animator.Play("Click");
        //    }
        //    else
        //    {
        //        _animator.Play("Normal");
        //    }
        //}
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        _pointIn = true;
        if (!isOn)
            _animator.Play("Highlighted");
        //if (!isOn)
        //    _animator.Play("Normal");
        //_pointIn = true;
        //if (!isOn)
        //    _animator.Play("Highlighted");
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        _pointIn = false;
        if (!isOn)
            _animator.Play("Normal");
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        //isOn = !isOn;
        //if (isOn)
        //{
        //    _animator.Play("Click");
        //}
        //else
        //{
        //    _animator.Play("Normal");
        //}
    }



    public void OnTogChanged(bool isActive)
    {
        _animator.Play(_tog.isOn ? "Highlighted" : "Normal");
        if (_tog.isOn)
            OnTogPlusSelected.Invoke(ToggleID);
    }

    public void Fadein()
    {
        _animator.Play("button-fadein");
    }

    [Serializable]
    public class TogglePlusEvent : UnityEvent<int>
    {

    }

}
