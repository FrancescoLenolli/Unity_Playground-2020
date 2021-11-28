using UIFramework.StateMachine;
using UnityEngine;

public class UIRoot_MainMenu : UIRoot
{
    [SerializeField]
    private UIView_MainMenu_Main mainMenuMain = null;

    public UIView_MainMenu_Main ViewMain { get => mainMenuMain; }
}
