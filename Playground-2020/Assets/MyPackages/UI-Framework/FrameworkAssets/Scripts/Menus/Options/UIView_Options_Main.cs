using System;
using UIFramework.StateMachine;

public class UIView_Options_Main : UIView
{
    private Action onCloseOptions;

    public Action OnCloseOptions { get => onCloseOptions; set => onCloseOptions = value; }

    public void CloseOptions()
    {
        onCloseOptions?.Invoke();
    }
}
