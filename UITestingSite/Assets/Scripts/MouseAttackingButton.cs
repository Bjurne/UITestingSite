using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAttackingButton : SmartButton
{
    [SerializeField] UIField bigBox = default; 

    //private void Start()
    //{
    //    ChangeBehaviour(OnSmartButtonPointerDownBaseBehaviour, OnSmartButtonHoverBeginBaseBehaviour);
    //}

    internal void OnSmartButtonHoverBeginAttackBehaviour()
    {
        iTween.MoveTo(gameObject, Input.mousePosition, 0.2f);

        var fourCornersArray = new Vector3[4];
        rectTransform.GetWorldCorners(fourCornersArray);
        for (int i = 0; i < fourCornersArray.Length; i++)
        {
            var corner = fourCornersArray[i];
            if (!bigBox.rectTransform.rect.Contains(corner))
            {
                ResetPosition();
            }
        }
    }
}
