using DemoRTS.Resources;
using System;
using System.Collections.Generic;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;

namespace DemoRTS.UI
{

    public struct UINotificationInfo
    {
        public bool canBeDisplayed;
        public string text;

        public UINotificationInfo(string messageText, bool canBeDisplayed)
        {
            string text = "";
            if (canBeDisplayed)
            {
                DateTime now = DateTime.Now; ;
                text = $"{now:HH:mm:ss}\n{messageText}";
            }

            this.canBeDisplayed = canBeDisplayed;
            this.text = text;
        }
    }

    public class UIView_ResourcesMenu_Main : UIView
    {
        [SerializeField] private List<TextMeshProUGUI> resourceLabels = new List<TextMeshProUGUI>();
        [SerializeField] private UINotification notificationPrefab = null;
        [SerializeField] private Transform notificationContainer = null;
        [SerializeField] private int maxNotification = 5;

        private Queue<UINotificationInfo> notifications = new Queue<UINotificationInfo>();

        private void Update()
        {
            if (notifications.Count > 0 && notificationContainer.childCount < maxNotification)
            {
                ShowNotification(notifications.Dequeue().text);
            }
        }

        public void UpdateResources(List<Resource> resources)
        {
            for (int i = 0; i < resources.Count; ++i)
                resourceLabels[i].text = resources[i].GetValue().ToString();
        }

        public void GetNotification(object value)
        {
            UINotificationInfo notification = (UINotificationInfo)value;

            if (notification.canBeDisplayed)
                notifications.Enqueue(notification);
        }

        private void ShowNotification(string notificationText)
        {
            if (!notificationPrefab) return;

            UINotification newNotification = Instantiate(notificationPrefab, notificationContainer);

            newNotification.ShowNotification(notificationText);
        }
    }
}