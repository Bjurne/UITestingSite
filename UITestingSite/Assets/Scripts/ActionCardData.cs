using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action Card", menuName = "Assets/ActionCards/ActionCard", order = 100)]
public class ActionCardData : ScriptableObject
{
    public int buildupTicks;
    public int ongoingTicks;
    public int recoveryTicks;

    public int damage;
}
