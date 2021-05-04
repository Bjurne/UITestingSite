using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptButton : MonoBehaviour
{
    [SerializeField] RectTransform choicesPanel;

    private List<Button> choices;
    private CanvasGroup canvasGroup;
    //private Action<int> onFinished;

    internal void Start()
    {
        //this.onFinished = onFinished;
        choices = new List<Button>(choicesPanel.GetComponentsInChildren<Button>());
        canvasGroup = choicesPanel.GetComponent<CanvasGroup>();
    }

    // TODO: this is a mess, bad job half done

    public void SendChoice(int choiceIndex)
    {
        ChoicePromptWidget.Instance.MakeChoice(choiceIndex);
    }
}
