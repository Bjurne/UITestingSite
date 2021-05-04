using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Challenge Data")]
public class ChallengeData : ScriptableObject
{
    public string title;
    public SolutionType[] solutions;
}

[System.Serializable]
public class SolutionType
{
    public SolutionTypes type;
    public int difficulty;
    public List<ChallengeModifierData> modifiers;
}
