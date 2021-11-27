using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIState_BuildingsMenu_Main : UIState_BuildingsMenu
{
    private UIView_BuildingsMenu_Main view;
    private BuildingManager buildingManager;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.mainView;
        buildingManager = root.buildingManager;
        view.OnSelectBuilding += SelectBuilding;

        view.SpawnButtons(buildingManager.buildingsCount);
    }

    private void SelectBuilding(int index)
    {
        buildingManager.InstantiateBuilding(index);
    }

}
