using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityObjectEvent : UnityEvent<object> { }

public class EventManager : MonoBehaviour
{
    private static EventManager eventManager;
    private Dictionary<string, UnityObjectEvent> eventDictionary;

    public static EventManager Instance { get { return GetInstance(); } }

    public static void StartListening(string eventName, UnityAction<object> listener)
    {
        UnityObjectEvent thisEvent = null;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityObjectEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (eventManager == null) return;

        UnityObjectEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            thisEvent.RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName, object argument)
    {
        UnityObjectEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            thisEvent.Invoke(argument);
    }

    private static EventManager GetInstance()
    {
        if (eventManager) return eventManager;

        eventManager = FindObjectOfType<EventManager>();
        if (!eventManager)
            Debug.LogError("There needs to be one active EventManager script on a GameObject in your Scene!");
        else
            eventManager.Init();

        return eventManager;
    }

    private void Init()
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<string, UnityObjectEvent>();
    }
}
