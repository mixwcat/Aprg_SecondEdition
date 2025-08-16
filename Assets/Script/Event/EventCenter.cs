using System;
using System.Collections.Generic;

public static class EventCenter
{
    private static Dictionary<string, Action<object>> eventDictionary = new Dictionary<string, Action<object>>();

    // 订阅事件
    public static void Subscribe(string eventName, Action<object> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = null;
        }
        eventDictionary[eventName] += listener;
    }

    // 取消订阅事件
    public static void Unsubscribe(string eventName, Action<object> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
    }

    // 触发事件
    public static void Trigger(string eventName, object parameter = null)
    {
        if (eventDictionary.ContainsKey(eventName) && eventDictionary[eventName] != null)
        {
            eventDictionary[eventName]?.Invoke(parameter);
        }
    }

    public static void ClearAll()
    {
        eventDictionary.Clear();
    }

    public static int GetSubscriberCount(string eventName)
    {
        if (eventDictionary.ContainsKey(eventName) && eventDictionary[eventName] != null)
        {
            return eventDictionary[eventName].GetInvocationList().Length;
        }
        return 0;
    }
}