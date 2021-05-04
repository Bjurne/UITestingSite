using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] TabGroup tabGroup = default;
    [SerializeField] internal Image backgroundImage = default;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    public void OnPointerDown(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        backgroundImage = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    internal void Deselect()
    {
        onTabDeselected?.Invoke();
    }

    internal void Select()
    {
        onTabSelected?.Invoke();
    }
}
