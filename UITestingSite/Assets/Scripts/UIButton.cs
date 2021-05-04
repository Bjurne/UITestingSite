using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button = default;
    [SerializeField] internal TextMeshProUGUI text = default;
    [SerializeField] internal Image plate = default;


    public bool IsBeingHovered { get { return isBeingHovered; } }

    private bool isBeingHovered;

    public void OnClickTest()
    {
        Debug.Log($"Det funkar!");
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isBeingHovered = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isBeingHovered = false;
    }
}
