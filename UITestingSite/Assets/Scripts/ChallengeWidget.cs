using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengeWidget : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fieldTitle = default;
    [SerializeField] RectTransform solutionsContainer = default;
    [SerializeField] GameObject solutionTypeWidgetPrefab = default;
    [SerializeField] GameObject challengeModifierWidgetPrefab = default;

    [SerializeField] RectTransform challengeModifiersContainer = default;
    [SerializeField] ChallengeData data = default;


    private void Start()
    {
        SetUp(data);
    }

    internal void SetUp(ChallengeData newData)
    {
        data = newData;
        fieldTitle.text = data.title;

        var modifiers = new List<ChallengeModifierData>();

        for (int i = 0; i < newData.solutions.Length; i++)
        {
            var go = Instantiate(solutionTypeWidgetPrefab, solutionsContainer);
            go.GetComponent<SolutionTypeWidget>().SetUp(newData.solutions[i]);
            for (int j = 0; j < newData.solutions[i].modifiers.Count; j++)
            {
                modifiers.Add(newData.solutions[i].modifiers[j]);
            }
        }
        foreach (ChallengeModifierData modifier in modifiers)
        {
            Debug.Log($"{modifier}");
        }
        //fieldSpecialModifiers.text = data.solutions[0].modifiers.ToString();
    }
}
