using Messaging;
using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIState_BuildingInfoMenu_Main : UIState_BuildingInfoMenu
{
    private UIView_BuildingInfoMenu_Main view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.mainView;
        MessagingSystem.StartListening("ShowBuildingInfo", ShowInfo);
        MessagingSystem.StartListening("HideBuildingInfo", HideInfo);
    }

    private void ShowInfo(object info)
    {
        ProductionBuildingInfo buildingInfo = (ProductionBuildingInfo)info;
        view.ShowView();
        view.SetInfo(buildingInfo);
    }

    private void HideInfo()
    {
        view.HideView();
    }
}
