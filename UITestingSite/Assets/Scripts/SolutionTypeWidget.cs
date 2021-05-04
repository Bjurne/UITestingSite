using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SolutionTypeWidget : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fieldType = default;
    [SerializeField] TextMeshProUGUI fieldDifficulty = default;
    private List<ChallengeModifierData> specialModifiers = default;

    internal void SetUp(SolutionType solution)
    {
        fieldType.text = solution.type.ToString();
        fieldDifficulty.text = solution.difficulty.ToString();
        specialModifiers = solution.modifiers;
    }
}

public enum SolutionTypes
{
    Dexterity = 0,
    Strength = 1,
}
