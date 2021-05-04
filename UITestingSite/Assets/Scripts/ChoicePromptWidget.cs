using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ChoicePromptWidget : MonoBehaviour
{
    public static ChoicePromptWidget Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private RectTransform backgroundRectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    private int chosenIndex;
    private Action<int> onFinished;
    private RectTransform originalParent;


    private void Start()
    {
        GetComponent<Image>().enabled = true;
        //canvasGroup = GetComponent<CanvasGroup>();
        backgroundRectTransform.SetParent(GetComponentInParent<Canvas>().rootCanvas.transform);
        backgroundRectTransform.anchorMin = new Vector2(0f,0f);
        backgroundRectTransform.anchorMax = new Vector2(1f,1f);
        backgroundRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        backgroundRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
        backgroundRectTransform.anchoredPosition = new Vector2(0,0);
        backgroundRectTransform.SetSiblingIndex(1);
        //rectTransform = GetComponent<RectTransform>();
        //rectTransform.anchorMin = new Vector2(0f,0f);
        //rectTransform.anchorMax = new Vector2(1f,1f);
        //CreateBackgroundPanel();
        SetVisible(false);
    }

    private void SetVisible(bool value)
    {
        canvasGroup.alpha = value ? 1f : 0f;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
        backgroundRectTransform.gameObject.SetActive(value);

        if (value)
        {
            originalParent = canvasGroup.GetComponentInParent<RectTransform>();
            canvasGroup.transform.SetParent(transform);
        }
        else
            canvasGroup.transform.SetParent(originalParent);
    }

    internal void ShowPrompt(CanvasGroup targetCanvasGroup, Action<int> onFinished)
    {
        canvasGroup = targetCanvasGroup;
        chosenIndex = 10;
        SetVisible(true);
        this.onFinished = onFinished;
    }

    public void MakeChoice(int chosenIndex)
    {
        SetVisible(false);
        if (chosenIndex == 10)
            return;
        onFinished?.Invoke(chosenIndex);
    }
}
