using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public abstract class GameEventOneParam<T> : ScriptableObject
    {
        private readonly List<GameEventListenerOneParam<T>> _listeners = new List<GameEventListenerOneParam<T>>();

        public void Register(GameEventListenerOneParam<T> l)
        {
            _listeners.Add(l);
        }

        public void UnRegister(GameEventListenerOneParam<T> l)
        {
            _listeners.Remove(l);
        }

        public void Raise(T val)
        {
            for (int i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].Response(val);
            }
        }
    }
}