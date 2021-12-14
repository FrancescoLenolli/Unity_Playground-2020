using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

namespace DemoRTS.UI
{
    public class UIState_BuildingsMenu : UIState
    {
        protected UIRoot_BuildingsMenu root;
        public override void PrepareState(UIStateMachine owner)
        {
            base.PrepareState(owner);
            if (!root)
                root = (UIRoot_BuildingsMenu)this.owner.Root;
        }
    }
}