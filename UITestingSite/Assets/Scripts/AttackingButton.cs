using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackingButton : UIButton
{
    private Vector2 mousePosition;
    private Vector2 originalPosition;

    private void Awake()
    {
        originalPosition = plate.transform.position;
    }

    void Update()
    {
        if (IsBeingHovered)
        {
            mousePosition = Input.mousePosition;
            text.transform.position = mousePosition;
            plate.transform.position = mousePosition;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        text.gameObject.transform.SetParent(GetComponentInParent<Canvas>().transform);
        plate.gameObject.transform.SetParent(GetComponentInParent<Canvas>().transform);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        text.gameObject.transform.SetParent(transform);
        plate.gameObject.transform.SetParent(transform);
        text.transform.position = originalPosition;
        plate.transform.position = originalPosition;
    }
}
