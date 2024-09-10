using System;
using System.Collections.Generic;

namespace CouchHero.Managers
{
    public class MessageManager
    {
        private static MessageManager _instance;

        public static MessageManager Instance
        {
            get
            {
                return _instance ??= new MessageManager();
            }
        }
        
        private Dictionary<string, Action<object[]>> _messageDict = new();
        private Dictionary<string, object[]> _dispatchCacheDict = new();
        
        public void Subscribe(string message, Action<object[]> action)
        {
            if (_messageDict.TryGetValue(message, out Action<object[]> value))
            {
                value += action;
                _messageDict[message] = value;
            }
            else
            {
                _messageDict.Add(message, action);
            }
        }

        public void UnSubscribe(string message)
        {
            _messageDict.Remove(message);
        }

        public void Dispatch(string message, object[] args = null, bool addToCache = false)
        {
            if (addToCache)
            {
                _dispatchCacheDict[message] = args;
            }
            else
            {
                if (_messageDict.TryGetValue(message, out Action<object[]> value))
                {
                    value.Invoke(args);
                }
            }
        }

        public void ProcessDispatchCache(string message)
        {
            if (!_dispatchCacheDict.TryGetValue(message, out object[] value)) return;
            Dispatch(message, value);
            _dispatchCacheDict.Remove(message);
        }
    }
}

