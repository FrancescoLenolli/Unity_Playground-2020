using UIFramework;
using UIFramework.StateMachine;
using UnityEngine;

public class UIState_Options_Main : UIState_Options
{
    private UIView_Options_Main view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.ViewMain;
        view.OnCloseOptions += CloseOptions;
    }

    public override void ShowState()
    {
        view.ShowView();
    }

    public override void HideState()
    {
        view.HideView();
    }

    private void CloseOptions()
    {
        Time.timeScale = 1;
        HideState();
        UIManager.Instance.OptionsOpen = false;
    }
}
