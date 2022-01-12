using System.Collections.Generic;
using UnityEngine.Events;

namespace Messaging
{
    public class UnityObjectEvent : UnityEvent<object> { }
    public static class MessagingSystem
    {
        private static Dictionary<string, UnityObjectEvent> objectEventDictionary = new Dictionary<string, UnityObjectEvent>();
        private static Dictionary<string, UnityEvent> emptyEventDictionary = new Dictionary<string, UnityEvent>();

        public static void StartListening(string eventName, UnityAction<object> listener)
        {
            UnityObjectEvent thisEvent = null;

            if (objectEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                if (thisEvent != null)
                {
                    thisEvent.AddListener(listener);
                }
                else
                {
                    thisEvent = new UnityObjectEvent();
                    thisEvent.AddListener(listener);
                    objectEventDictionary[eventName] = thisEvent;
                }
            }
            else
            {
                thisEvent = new UnityObjectEvent();
                thisEvent.AddListener(listener);
                objectEventDictionary.Add(eventName, thisEvent);
            }

        }

        public static void StartListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;

            if (emptyEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                emptyEventDictionary.Add(eventName, thisEvent);
            }

        }

        public static void StopListening(string eventName, UnityAction<object> listener)
        {
            UnityObjectEvent thisEvent = null;
            if (objectEventDictionary.TryGetValue(eventName, out thisEvent))
                thisEvent.RemoveListener(listener);
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (emptyEventDictionary.TryGetValue(eventName, out thisEvent))
                thisEvent.RemoveListener(listener);
        }

        public static void TriggerEvent(string eventName, object argument)
        {
            UnityObjectEvent thisEvent = null;
            if (objectEventDictionary.TryGetValue(eventName, out thisEvent))
                thisEvent?.Invoke(argument);
            else
                objectEventDictionary.Add(eventName, thisEvent);
        }

        public static void TriggerEvent(string eventName)
        {
            UnityEvent thisEvent = null;
            if (emptyEventDictionary.TryGetValue(eventName, out thisEvent))
                thisEvent?.Invoke();
            else
                emptyEventDictionary.Add(eventName, thisEvent);
        }
    }
}
