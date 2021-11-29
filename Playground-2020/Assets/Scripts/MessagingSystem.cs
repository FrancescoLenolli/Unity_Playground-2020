using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Messaging
{
    public class UnityObjectEvent : UnityEvent<object> { }
    public static class MessagingSystem
    {
        private static Dictionary<string, UnityObjectEvent> eventDictionary = new Dictionary<string, UnityObjectEvent>();

        public static void StartListening(string eventName, UnityAction<object> listener)
        {
            UnityObjectEvent thisEvent = null;

            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityObjectEvent();
                thisEvent.AddListener(listener);
                eventDictionary.Add(eventName, thisEvent);
            }

        }

        public static void StopListening(string eventName, UnityAction<object> listener)
        {
            UnityObjectEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
                thisEvent.RemoveListener(listener);
        }

        public static void TriggerEvent(string eventName, object argument)
        {
            UnityObjectEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
                thisEvent?.Invoke(argument);
            else
                eventDictionary.Add(eventName, thisEvent);
        }
    }
}
