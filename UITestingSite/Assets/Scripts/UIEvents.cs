using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    #region Singleton
    public static UIEvents Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public Action<DiceResult> OnRollerResultReported;
    public Action OnStartRollers;

    public void TriggerEvent(UIEvent key)
    {
        switch (key)
        {
            case UIEvent.ChallengeStartRollers:
                OnStartRollers?.Invoke();
                break;
            case UIEvent.ChallengeReportRollerResult:
                // TODO: is there a way to create a generic trigger for events with special parameters?
                break;
            default:
                break;
        }
    }

    public void TriggerRollerResultReported(DiceResult result)
    {
        OnRollerResultReported?.Invoke(result);
    }
}

public enum UIEvent
{
    ChallengeStartRollers = 0,
    ChallengeReportRollerResult = 1,
}
