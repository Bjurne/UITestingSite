using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Challenge Modifier Data")]
public class ChallengeModifierData : ScriptableObject
{
    public List<DiceResult> diceFaces;
    public string challengeEvents;
}
