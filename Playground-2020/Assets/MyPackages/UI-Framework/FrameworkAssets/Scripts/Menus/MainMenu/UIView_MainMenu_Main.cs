using System;
using UIFramework.StateMachine;

public class UIView_MainMenu_Main : UIView
{
    private Action onStartGame;
    private Action onOpenOptions;
    private Action onQuitGame;

    public Action OnStartGame { get => onStartGame; set => onStartGame = value; }
    public Action OnOpenOptions { get => onOpenOptions; set => onOpenOptions = value; }
    public Action OnQuitGame { get => onQuitGame; set => onQuitGame = value; }

    public void StartGame()
    {
        onStartGame?.Invoke();
    }

    public void OpenOptions()
    {
        onOpenOptions?.Invoke();
    }

    public void QuitGame()
    {
        onQuitGame?.Invoke();
    }
}
