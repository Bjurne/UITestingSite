using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(RectTransform))]
public class SmartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] Button button = default;
    [SerializeField] internal TextMeshProUGUI text = default;
    [SerializeField] internal Image plate = default;
    [SerializeField] CanvasGroup canvasGroup = default;
    [SerializeField] internal RectTransform rectTransform = default;

    public static Action OnSmartButtonPointerDown;
    public static Action OnSmartButtonHoverBegin;

    private Action CurrentOnSmartButtonPointerDownBehaviour;
    private Action CurrentOnSmartButtonHoverBeginBehaviour;


    public bool IsBeingHovered { get { return isBeingHovered; } }

    private bool isBeingHovered;
    private Vector2 originalPosition;

    private void Start()
    {
        originalPosition = rectTransform.position;
        Debug.Log($"{originalPosition}");
        ChangeBehaviour(OnSmartButtonPointerDownBaseBehaviour, OnSmartButtonHoverBeginBaseBehaviour);
    }

    internal void ResetPosition()
    {
        rectTransform.position = originalPosition;
    }

    internal void ChangeBehaviour(Action newPointerDownBehaviour = null, Action newHoverBeginBehaviour = null)
    {
        if (newPointerDownBehaviour != null)
        {
            if (CurrentOnSmartButtonPointerDownBehaviour != null)
                OnSmartButtonPointerDown -= CurrentOnSmartButtonPointerDownBehaviour;

            CurrentOnSmartButtonPointerDownBehaviour = newPointerDownBehaviour;
            OnSmartButtonPointerDown += newPointerDownBehaviour;
        }
        if (newHoverBeginBehaviour != null)
        {
            if (CurrentOnSmartButtonHoverBeginBehaviour != null)
                OnSmartButtonHoverBegin -= CurrentOnSmartButtonHoverBeginBehaviour;

            CurrentOnSmartButtonHoverBeginBehaviour = newHoverBeginBehaviour;
            OnSmartButtonHoverBegin += newHoverBeginBehaviour;
        }
    }

    private void OnEnable()
    {
        OnSmartButtonPointerDown += OnSmartButtonPointerDownBaseBehaviour;
        CurrentOnSmartButtonPointerDownBehaviour = OnSmartButtonPointerDownBaseBehaviour;
        OnSmartButtonHoverBegin += OnSmartButtonHoverBeginBaseBehaviour;
        CurrentOnSmartButtonHoverBeginBehaviour = OnSmartButtonHoverBeginBaseBehaviour;
    }

    private void OnDisable()
    {
        OnSmartButtonPointerDown -= CurrentOnSmartButtonPointerDownBehaviour;
        OnSmartButtonHoverBegin -= CurrentOnSmartButtonHoverBeginBehaviour;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isBeingHovered = true;
        OnSmartButtonHoverBegin?.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isBeingHovered = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSmartButtonPointerDown?.Invoke();
    }

    public void OnSmartButtonPointerDownBaseBehaviour()
    {
        Debug.Log($"Klick BaseBehavior");
    }

    public void OnSmartButtonHoverBeginBaseBehaviour()
    {
        Debug.Log($"hovering began");
    }

    public void OnSmartButtonHoverBeginEvadeBehaviour()
    {
        Debug.Log($"hovering began");
        EvadePointer();
    }

    public void OnSmartButtonPointerDownEvadeBehaviour()
    {
        EvadePointer();
    }

    private void OnSmartButtonPointerDownMultiClickBehaviour()
    {
        StartCoroutine(MultiClickSequence());
    }

    private IEnumerator MultiClickSequence()
    {
        for (int i = 0; i < UnityEngine.Random.Range(2, 6); i++)
        {
            Debug.Log($"Klick!");
            var shakePower = new Vector3(UnityEngine.Random.Range(0.2f, 2f), UnityEngine.Random.Range(0.2f, 2f), 0f);
            iTween.ShakeScale(gameObject, shakePower, 0.1f);
            yield return new WaitForSeconds(0.2f);
        }
        ChangeBehaviour(OnSmartButtonPointerDownBaseBehaviour);
    }

    private void EvadePointer()
    {
        SetInteractable(false);

        var evadeVector = (rectTransform.position - Input.mousePosition) * 2f;

        // TODO: Setup wrapper for itween hash creation?
        var hash = iTween.Hash("amount", evadeVector, "time", 0.2f, "oncomplete", "SetInteractable", "oncompleteparams", true);
        iTween.MoveBy(gameObject, hash);

        var x = rectTransform.position.x + evadeVector.x;
        var y = rectTransform.position.y + evadeVector.y;
        if ((x < 0f || x > Screen.width) || (y < 0f || y > Screen.height))
        {
            ResetPosition();
        }
    }

    private void SetInteractable(bool value)
    {
        canvasGroup.interactable = value;
    }
}