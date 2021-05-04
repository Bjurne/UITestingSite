using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIField : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] MouseAttackingButton connectedSmartButton = default;
    private bool isBeingHovered;

    internal RectTransform rectTransform { get; private set; }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        if (isBeingHovered)
        {
            connectedSmartButton.OnSmartButtonHoverBeginAttackBehaviour();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isBeingHovered = true;
        //connectedSmartButton.OnSmartButtonHoverBeginAttackBehaviour();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        isBeingHovered = false;
        connectedSmartButton.ResetPosition();
    }
}
