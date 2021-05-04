using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceResultContainerWidget : MonoBehaviour
{
    [SerializeField] Image resultImage = default;
    [SerializeField] Vector3 animateFromPosition;

    DiceResult result;
    Vector3 toPos;
    Vector3 fromPos;

    internal void Setup()
    {
        toPos = transform.position;
        fromPos = transform.position + animateFromPosition;
    }

    internal void DisplayResult(DiceResult newResult, float time)
    {
        transform.position = toPos;
        result = newResult;
        resultImage.sprite = result.resultSprite;

        iTween.MoveFrom(gameObject, fromPos, time);
    }
}
