using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeResultWidget : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI fieldResultValue = default;
    [SerializeField] RectTransform symbolsContainer = default;
    DiceBarWidget barWidget;
    List<DiceRollerWidget> rollerWidgets => GetComponentInParent<DiceBarWidget>().widgets;

    private int totalScore = 0;
    private int activeRollers = 0;

    private void Start()
    {
        UIEvents.Instance.OnStartRollers += ChallengeRollStarted;
        UIEvents.Instance.OnRollerResultReported += OnDiceResult;
    }

    internal void ChallengeRollStarted()
    {
        totalScore = 0;
        activeRollers = 0;
        fieldResultValue.text = totalScore.ToString();
        for (int i = 0; i < rollerWidgets.Count; i++)
        {
            //rollerWidgets[i].OnDiceResult += OnDiceResult;
            activeRollers++;
        }
    }

    private void Clear()
    {
        for (int i = 0; i < rollerWidgets.Count; i++)
        {
            //rollerWidgets[i].OnDiceResult -= OnDiceResult;
        }
    }

    internal void OnDisable()
    {
        //Clear();
    }

    private void OnDiceResult(DiceResult result)
    {
        switch (result.resultValue)
        {
            case DiceResultValue.Blank:
                break;
            case DiceResultValue.One:
                totalScore += 1;

                break;
            case DiceResultValue.Two:
                totalScore += 2;

                break;
            case DiceResultValue.Three:
                totalScore += 3;

                break;
            case DiceResultValue.ExclamationMark:
                Debug.Log($"add ! sprite");
                break;
            case DiceResultValue.Special:
                break;
            default:
                break;
        }
        fieldResultValue.text = totalScore.ToString();
        activeRollers--;

        if (activeRollers <= 0)
        {
            Clear();
        }
    }
}
