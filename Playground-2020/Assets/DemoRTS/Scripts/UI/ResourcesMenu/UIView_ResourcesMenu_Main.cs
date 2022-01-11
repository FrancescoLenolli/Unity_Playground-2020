using DemoRTS.Resources;
using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;

namespace DemoRTS.UI
{
    public class UIView_ResourcesMenu_Main : UIView
    {
        [SerializeField] private List<TextMeshProUGUI> resourceLabels = new List<TextMeshProUGUI>();
        [SerializeField] private UINotification notificationPrefab = null;
        [SerializeField] private Transform notificationContainer = null;

        private Queue<string> messages = new Queue<string>();

        private void Update()
        {
            if(messages.Count > 0)
            {
                ShowNotification(messages.Dequeue());
            }
        }

        public void UpdateResources(List<Resource> resources, string text)
        {
            for (int i = 0; i < resources.Count; ++i)
                resourceLabels[i].text = resources[i].GetValue().ToString();

            messages.Enqueue(text);
        }

        private void ShowNotification(string notificationText)
        {
            if (!notificationPrefab) return;

            UINotification newNotification = Instantiate(notificationPrefab, notificationContainer);

            newNotification.ShowNotification(notificationText);
        }
    }
}