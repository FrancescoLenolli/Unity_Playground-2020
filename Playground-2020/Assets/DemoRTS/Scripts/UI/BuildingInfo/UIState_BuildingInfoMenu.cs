using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

namespace DemoRTS.UI
{
    public class UIState_BuildingInfoMenu : UIState
    {
        protected UIRoot_BuildingInfoMenu root;
        public override void PrepareState(UIStateMachine owner)
        {
            base.PrepareState(owner);
            if (!root)
                root = (UIRoot_BuildingInfoMenu)this.owner.Root;
        }
    }
}