using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIState_ResourcesMenu : UIState
{
    protected UIRoot_ResourcesMenu root;
    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        if (!root)
            root = (UIRoot_ResourcesMenu)this.owner.Root;
    }
}
