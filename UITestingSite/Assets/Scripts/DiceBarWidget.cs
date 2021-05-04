using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBarWidget : MonoBehaviour
{
    [SerializeField] RectTransform rollerContainer;
    [SerializeField] GameObject rollerPrefab;
    [SerializeField] DiceData levelZeroDie;
    [SerializeField] DiceData levelOneDie;
    [SerializeField] PromptButton addRollerPromptButton = default;
    [SerializeField] PromptButton removeRollerPromptButton = default;
    internal List<DiceRollerWidget> widgets { get; private set; }
    LayoutGroup layoutGroup;
    private DiceData chosenDiceData;

    private void Start()
    {
        widgets = new List<DiceRollerWidget>(rollerContainer.GetComponentsInChildren<DiceRollerWidget>());

        layoutGroup = rollerContainer.GetComponent<LayoutGroup>();
        layoutGroup.enabled = true;
        chosenDiceData = ScriptableObject.CreateInstance<DiceData>();
        chosenDiceData = levelZeroDie;

        foreach (DiceRollerWidget wid in widgets)
        {
            wid.SetUp(chosenDiceData);
        }
    }

    public void RollDebug()
    {
        UIEvents.Instance.TriggerEvent(UIEvent.ChallengeStartRollers);
        foreach (DiceRollerWidget wid in widgets)
        {
            wid.RollDice();
        }
    }

    public void AddDiceRoller(CanvasGroup addRollerChoicesCanvasGroup)
    {
        ChoicePromptWidget.Instance.ShowPrompt(addRollerChoicesCanvasGroup, OnDiceTypeToAddChosen);
        //var go = Instantiate(rollerPrefab, rollerContainer, false);
        //var widget = go.GetComponent<DiceRollerWidget>();
        //widgets.Add(widget);
        //StartCoroutine(RollerSetUpRoutine(widget));

        //rollerContainer.sizeDelta = new Vector2(widgets.Count * 100f, 200f);
    }

    private void OnDiceTypeToAddChosen(int chosenIndex)
    {
        if (chosenIndex == 0)
            chosenDiceData = levelZeroDie;
        if (chosenIndex == 1)
        {
            chosenDiceData = levelOneDie;
        }

        var go = Instantiate(rollerPrefab, rollerContainer, false);
        var widget = go.GetComponent<DiceRollerWidget>();
        widgets.Add(widget);
        StartCoroutine(RollerSetUpRoutine(widget));

        rollerContainer.sizeDelta = new Vector2(widgets.Count * 100f, 200f);
    }

    private void OnDiceTypeToRemoveChosen(int chosenIndex)
    {
        DiceData diceDataToRemove = null;
        if (chosenIndex == 0)
            diceDataToRemove = levelZeroDie;
        else if (chosenIndex == 1)
            diceDataToRemove = levelOneDie;
        else
            return;

        var item = widgets.Find(p => p.diceData == diceDataToRemove);
        if (item != null)
        {
            widgets.Remove(item);
            Destroy(item.gameObject);
        }

        rollerContainer.sizeDelta = new Vector2(widgets.Count * 100f, 200f);

        foreach (DiceRollerWidget widget in widgets)
        {
            StartCoroutine(RollerSetUpRoutine(widget));
        }
    }

    private IEnumerator RollerSetUpRoutine(DiceRollerWidget widget)
    {
        yield return new WaitForEndOfFrame();
        widget.SetUp(chosenDiceData);
    }

    public void RemoveDiceRoller(CanvasGroup removeRollerChoicesCanvasGroup)
    {
        ChoicePromptWidget.Instance.ShowPrompt(removeRollerChoicesCanvasGroup, OnDiceTypeToRemoveChosen);

        //if (widgets.Count > 0)
        //{
        //    var item = widgets[widgets.Count - 1];
        //    widgets.Remove(item);
        //    Destroy(item.gameObject);
        //}

        //rollerContainer.sizeDelta = new Vector2(widgets.Count * 100f, 200f);
    }
}
