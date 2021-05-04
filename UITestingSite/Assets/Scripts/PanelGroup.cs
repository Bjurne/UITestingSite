using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    [SerializeField] GameObject[] panels = default;
    [SerializeField] TabGroup tabGroup = default;

    private int panelIndex;

    private void Awake()
    {
        ShowCurrentPanel();
    }

    private void ShowCurrentPanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(panelIndex == i);
        }
    }

    internal void SetPageIndex(int index)
    {
        Debug.Log($"Showing Panel {index}");
        panelIndex = index;
        ShowCurrentPanel();
    }
}
