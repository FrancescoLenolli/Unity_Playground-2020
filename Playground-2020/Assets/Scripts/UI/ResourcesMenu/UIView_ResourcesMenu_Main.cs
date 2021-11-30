using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;

public class UIView_ResourcesMenu_Main : UIView
{
    [SerializeField] private List<TextMeshProUGUI> resourceLabels = new List<TextMeshProUGUI>();

    public void UpdateResources(List<Resource> resources)
    {
        for (int i = 0; i < resources.Count; ++i)
            resourceLabels[i].text = resources[i].GetValue().ToString();
    }
}
