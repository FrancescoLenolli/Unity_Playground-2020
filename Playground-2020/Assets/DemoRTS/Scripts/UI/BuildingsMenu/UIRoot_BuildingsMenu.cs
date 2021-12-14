using DemoRTS.Buildings;
using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

namespace DemoRTS.UI
{
    public class UIRoot_BuildingsMenu : UIRoot
    {
        public BuildingManager buildingManager = null;
        public UIView_BuildingsMenu_Main mainView = null;
    }
}