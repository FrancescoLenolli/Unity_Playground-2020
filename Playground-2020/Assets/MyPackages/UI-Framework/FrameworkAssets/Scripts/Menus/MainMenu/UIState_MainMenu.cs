using UIFramework.StateMachine;

public class UIState_MainMenu : UIState
{
    protected UIRoot_MainMenu root;
    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        if (!root)
            root = (UIRoot_MainMenu)this.owner.Root;
    }
}
