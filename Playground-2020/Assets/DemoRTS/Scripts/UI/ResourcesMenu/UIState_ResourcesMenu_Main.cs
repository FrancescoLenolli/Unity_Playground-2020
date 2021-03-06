using DemoRTS.Resources;
using Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

namespace DemoRTS.UI
{
    public class UIState_ResourcesMenu_Main : UIState_ResourcesMenu
    {
        private UIView_ResourcesMenu_Main view;

        public override void PrepareState(UIStateMachine owner)
        {
            base.PrepareState(owner);
            ResourceManager resourceManager = root.resourceManager;
            view = root.mainView;
            MessagingSystem.StartListening("ResourcesUpdated", UpdateResources);
            MessagingSystem.StartListening("NotificationSent", view.GetNotification);
        }

        private void UpdateResources(object resources)
        {
            List<Resource> updatedResources = (List<Resource>)resources;

            view.UpdateResources(updatedResources);
        }
    }
}