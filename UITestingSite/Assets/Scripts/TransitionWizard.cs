using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWizard : MonoBehaviour
{
    [SerializeField] RectTransform guideRectTransform = default;
    [SerializeField] RectTransform rectTransform = default;

    private float distance => Vector2.Distance(rectTransform.position, guideRectTransform.position);

    public float transitionTime = 1f;
    public float punchStrength;

    public void GuidedTransitionTrigger() => StartCoroutine(GuidedTransition());

    public void GuidedTweenTrigger() => StartCoroutine(GuidedTween());

    private IEnumerator GuidedTween()
    {
        var startPos = rectTransform.position;
        var targetPos = guideRectTransform.position;
        var startRotation = rectTransform.rotation;
        var targetRotation = guideRectTransform.rotation;
        var startRect = rectTransform.rect;
        var targetRect = guideRectTransform.rect;
        var startOriginalScale = rectTransform.localScale;
        var targetOriginalScale = rectTransform.localScale;

        iTween.MoveTo(rectTransform.gameObject, guideRectTransform.position, transitionTime);
        iTween.PunchPosition(rectTransform.gameObject, Vector3.one * punchStrength, transitionTime);
        var targetScale = new Vector3(guideRectTransform.rect.width / rectTransform.rect.width, guideRectTransform.rect.height / rectTransform.rect.height, 1f);
        iTween.RotateTo(rectTransform.gameObject, guideRectTransform.eulerAngles, transitionTime);
        iTween.ScaleTo(rectTransform.gameObject, targetScale, transitionTime);

        yield return new WaitForSeconds(transitionTime * 1.05f);

        //rectTransform.position = targetPos;
        rectTransform.localScale = targetOriginalScale;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetRect.width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetRect.height);
        rectTransform.SetPositionAndRotation(targetPos, targetRotation);

        //guideRectTransform.position = startPos;
        guideRectTransform.localScale = startOriginalScale;
        guideRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startRect.width);
        guideRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, startRect.height);
        guideRectTransform.SetPositionAndRotation(startPos, startRotation);

    }

    private IEnumerator GuidedTransition()
    {
        var startPos = rectTransform.position;
        var targetPos = guideRectTransform.position;
        var startRect = rectTransform.rect;
        var targetRect = guideRectTransform.rect;
        var timer = 0f;
        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            Vector2 newPos = Vector2.Lerp(startPos, targetPos, timer / transitionTime);
            var newWidth = Mathf.Lerp(startRect.width, targetRect.width, timer / transitionTime);
            var newHeight = Mathf.Lerp(startRect.height, targetRect.height, timer / transitionTime);
            rectTransform.transform.position = newPos;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
            yield return new WaitForSeconds(0.05f);
        }

        rectTransform.position = targetPos;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetRect.width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetRect.height);

        guideRectTransform.position = startPos;
        guideRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startRect.width);
        guideRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, startRect.height);


        yield return null;
    }



    private void OnValidate()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }


        if (guideRectTransform == null)
        {
            if (GetComponentInChildren<TransitionGuideRect>())
            {
                guideRectTransform = GetComponentInChildren<TransitionGuideRect>().rectTransform;
            }
            else
            {
                GameObject childObject = new GameObject("Transition Guide Rect");
                childObject.transform.SetParent(this.transform, false);
                var guide = childObject.AddComponent<TransitionGuideRect>();
                guideRectTransform = guide.rectTransform;

                CopyRectTransform(rectTransform, guideRectTransform);
            }
        }
    }

    private void CopyRectTransform(RectTransform original, RectTransform copy)
    {
        copy.anchorMin = original.anchorMin;
        copy.anchorMax = original.anchorMax;
        copy.sizeDelta = original.sizeDelta;
        copy.pivot = original.pivot;

        //copy.rect.Set(original.rect.x, original.rect.y, original.rect.width, original.rect.height);

        //copy.anchoredPosition = original.anchoredPosition;
        //copy.localPosition = Vector3.zero;
    }
}
