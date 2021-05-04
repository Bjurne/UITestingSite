using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollerWidget : MonoBehaviour
{
    //[SerializeField] DiceResult[] resultsArray;
    private Queue<DiceResult> results = default;
    [SerializeField] DiceResultContainerWidget incomingResultContainer = default;
    [SerializeField] DiceResultContainerWidget currentResultContainer = default;
    [SerializeField] DiceResultContainerWidget previousResultContainer = default;

    private DiceResult previousResult;
    private DiceResult currentResult;
    private DiceResult incomingResult;
    private DiceBarWidget parentBarWidget;
    internal DiceData diceData { get; private set; }

    private void Start() // TODO: cheating in order for instantiating to work properly
    {
        parentBarWidget = GetComponentInParent<DiceBarWidget>();
        //Invoke("SetUp", 1f);
    }

    internal void SetUp(DiceData newDiceData)
    {
        incomingResultContainer.Setup();
        currentResultContainer.Setup();
        previousResultContainer.Setup();

        results = new Queue<DiceResult>();
        diceData = newDiceData;
        var resultsArray = diceData.faces.ToArray();
        foreach (DiceResult result in resultsArray)
        {
            results.Enqueue(result);
        }
        //currentResult = resultsArray[0];
        incomingResult = resultsArray[1];
        incomingResultContainer.DisplayResult(incomingResult, 0f);
        currentResult = resultsArray[0];
        currentResultContainer.DisplayResult(currentResult, 0f);
        previousResult = resultsArray[resultsArray.Length - 1];
        previousResultContainer.DisplayResult(previousResult, 0f);
    }

    public void RollDice()
    {
        //if (currentResult == null)
        //    currentResult = results.Dequeue();

        StartCoroutine(RollDiceRoutine());
    }

    private IEnumerator RollDiceRoutine()
    {
        //if (currentResult == results[results.Length -1])
        //    currentResult = results[0];
        //else
        //    currentResult = results[]
        var rollTimerFull = UnityEngine.Random.Range(1f, 3f);
        var rollsToMake = UnityEngine.Random.Range(8f, 20f);

        var rollFraction = rollTimerFull / rollsToMake;
        var rollFractionFull = rollFraction;

        for (int i = 0; i < rollsToMake; i++)
        {
            var percentageDone = i / rollsToMake;
            rollFraction = Mathf.Lerp(rollFractionFull / 2f, rollFractionFull, percentageDone);
            UpdateResults(rollFraction / 2f);
            yield return new WaitForSeconds(rollFraction);
        }

        var punchPower = Mathf.Clamp((int)currentResult.resultValue + 0.1f, 0f, 1.6f); // TODO: quickly changed with new DiceResultValue enums, validate
        iTween.PunchScale(currentResultContainer.gameObject, Vector2.one * punchPower, 1f);
        UIEvents.Instance.TriggerRollerResultReported(currentResult);
        yield return currentResult;
    }

    private void UpdateResults(float speed)
    {
        currentResult = results.Dequeue();
        results.Enqueue(currentResult);

        incomingResult = results.Peek();

        previousResultContainer.DisplayResult(previousResult, speed);
        currentResultContainer.DisplayResult(currentResult, speed);
        incomingResultContainer.DisplayResult(incomingResult, speed);
        previousResult = currentResult;
    }
}

[System.Serializable]
public class DiceResult
{
    [SerializeField] internal Sprite resultSprite;
    [SerializeField] internal DiceResultValue resultValue;
}

public enum DiceResultValue
{
    Blank,
    One = 1,
    Two = 2,
    Three = 3,
    ExclamationMark = 4,
    Special = 5,
}

[CreateAssetMenu(menuName = "Assets/Dice Data")]
public class DiceData : ScriptableObject
{
    public List<DiceResult> faces;
}
