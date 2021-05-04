using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCard : MonoBehaviour
{
    [SerializeField] private ActionCardData data;
    [SerializeField] private Image[] counters;

    private int buildupTicks;
    private int ongoingTicks;
    private int recoveryTicks;
    private int damage;

    private void Awake()
    {
        Reset();
    }

    private void Reset()
    {
        buildupTicks = data.buildupTicks;
        ongoingTicks = data.ongoingTicks;
        recoveryTicks = data.recoveryTicks;

        damage = data.damage;
        UpdateCounters();
    }

    public void TickDown()
    {
        if (buildupTicks > 0)
            buildupTicks--;

        else if (ongoingTicks > 0)
        {
            ongoingTicks--;
            Debug.Log($"{damage} damage dealt");
        }

        else if (recoveryTicks > 0)
            recoveryTicks--;

        else
            Reset();

        UpdateCounters();
    }

    private void UpdateCounters()
    {
        for (int i = 0; i < counters.Length; i++)
        {
            counters[i].gameObject.SetActive(false);
        }

        var totalActiveCounters = buildupTicks + ongoingTicks + recoveryTicks;

        for (int i = 0; i < totalActiveCounters; i++)
        {
            counters[i].gameObject.SetActive(true);

            if (i < buildupTicks)
                counters[i].color = Color.yellow;

            else if (i >= buildupTicks + ongoingTicks)
                counters[i].color = Color.red;

            else
                counters[i].color = Color.green;
        }


    }
}
