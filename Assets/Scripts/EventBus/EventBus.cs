using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus
{
    public class EventBus
    {
        private Hashtable _events = new();
        
        private static EventBus _instance;

        public static EventBus Instance => _instance ??= new();

        public void AddListener<T>(EventName name, Action<T> callback)
        {
            string key = GetKey<T>(name);

            Action<T> currentEvent = null;

            if (_events.ContainsKey(key))
            {
                currentEvent = (Action<T>)_events[key];
                currentEvent += callback;
                _events[key] = currentEvent;
            }
            else
            {
                currentEvent += callback;
                _events.Add(key, currentEvent);
            }
        }
        
        public void RemoveListener<T>(EventName name, Action<T> callback)
        {
            Action<T> currentEvent = null;
            string key = GetKey<T>(name);

            if (_events.ContainsKey(key))
            {
                currentEvent = (Action<T>)_events[key];
                currentEvent -= callback;
                _events[key] = currentEvent;
            }
        }

        public void TriggerEvent<T>(EventName name, T returnedType)
        {
            Action<T> currentEvent;
            string key = GetKey<T>(name);

            if (_events.ContainsKey(key))
            {
                currentEvent = (Action<T>)_events[key];
                currentEvent.Invoke(returnedType);
            }
        }

        private string GetKey<T>(EventName eventName)
        {
            Type type = typeof(T);
            string key = type + eventName.ToString();
            return key;
        }
    }
}