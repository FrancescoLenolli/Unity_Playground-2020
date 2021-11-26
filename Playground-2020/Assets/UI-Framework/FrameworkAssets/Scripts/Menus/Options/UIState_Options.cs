using UIFramework.StateMachine;

public class UIState_Options : UIState
{
    protected UIRoot_Options root;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        if (!root)
            root = (UIRoot_Options)owner.Root;
    }
}
