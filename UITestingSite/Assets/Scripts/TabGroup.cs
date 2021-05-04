using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] List<TabButton> tabButtons = default;
    [SerializeField] Color tabIdle = default;
    [SerializeField] Color tabHovered = default;
    [SerializeField] Color tabActive = default;
    [SerializeField] List<GameObject> objectsToSwap = default;
    [SerializeField] PanelGroup panelGroup = default;

    private TabButton selectedTab;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);

        if (selectedTab == null)
            OnTabSelected(button);

        ResetAllTabs();
    }

    public void OnTabEnter(TabButton button)
    {
        ResetAllTabs();
        if (selectedTab == null || button != selectedTab)
            button.backgroundImage.color = tabHovered;
    }
    public void OnTabExit(TabButton button)
    {
        ResetAllTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null)
            selectedTab.Deselect();

        selectedTab = button;
        selectedTab.Select();

        ResetAllTabs();
        button.backgroundImage.color = tabActive;

        var index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
                objectsToSwap[i].SetActive(true);
            else
                objectsToSwap[i].SetActive(false);
        }

        if (panelGroup != null)
            panelGroup.SetPageIndex(index);
    }

    private void ResetAllTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
                continue;
            button.backgroundImage.color = tabIdle;
        }
    }
}
