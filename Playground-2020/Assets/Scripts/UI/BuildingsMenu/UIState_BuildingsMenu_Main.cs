using Messaging;
using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIState_BuildingsMenu_Main : UIState_BuildingsMenu
{
    private UIView_BuildingsMenu_Main view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.mainView;
        view.OnSelectBuilding += SelectBuilding;

        view.SpawnButtons(root.buildingManager.buildingsCount);
    }

    private void SelectBuilding(int index)
    {
        MessagingSystem.TriggerEvent("SelectBuilding", index);
    }

}
