using System;
using System.Collections.Generic;
using UnityEngine;

public enum SimpleEventType
{
    PlayerHealthChangedEvent,
    PlayerDeathEvent
}

public static class EventManager
{
    private static readonly Dictionary<Type, Delegate> EventsWithData = new Dictionary<Type, Delegate>();
    private static readonly Dictionary<SimpleEventType, Action> EventsWithoutData = new Dictionary<SimpleEventType, Action>();
    
    public static void Subscribe<T>(Action<T> listener) where T : struct
    {
        if (EventsWithData.ContainsKey(typeof(T)))  
        {
            EventsWithData[typeof(T)] = Delegate.Combine(EventsWithData[typeof(T)], listener);
        }
        else
        {
            EventsWithData[typeof(T)] = listener;
        }
    }

    public static void Subscribe(SimpleEventType eventType, Action listener)
    {
        if (EventsWithoutData.ContainsKey(eventType))
        {
            EventsWithoutData[eventType] += listener;
        }
        else
        {
            EventsWithoutData[eventType] = listener;
        }
    }
    
    public static void Unsubscribe<T>(Action<T> listener) where T : struct
    {
        if (EventsWithData.ContainsKey(typeof(T)))
        {
            EventsWithData[typeof(T)] = Delegate.Remove(EventsWithData[typeof(T)], listener);

            if (EventsWithData[typeof(T)] == null)
            {
                EventsWithData.Remove(typeof(T));
            }
        }
    }

    public static void Unsubscribe(SimpleEventType eventType, Action listener)
    {
        if (!EventsWithoutData.ContainsKey(eventType)) return;

        EventsWithoutData[eventType] -= listener;

        if (EventsWithoutData[eventType] == null)
        {
            EventsWithoutData.Remove(eventType);
        }
    }


    public static void Trigger<T>(T eventData) where T : struct
    {
        if (EventsWithData.ContainsKey(typeof(T)) && EventsWithData[typeof(T)] != null)
        {
            (EventsWithData[typeof(T)] as Action<T>)?.Invoke(eventData);
        }
    }

    public static void Trigger(SimpleEventType eventType)
    {
        if (EventsWithoutData.ContainsKey(eventType) && EventsWithoutData[eventType] != null)
        {
            EventsWithoutData[eventType]();
        }
    }
  
}
