using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public abstract class GameEventOneParam<T> : ScriptableObject
    {
        List<GameEventListenerOneParam<T>> listeners = new List<GameEventListenerOneParam<T>>();

        public void Register(GameEventListenerOneParam<T> l)
        {
            listeners.Add(l);
        }

        public void UnRegister(GameEventListenerOneParam<T> l)
        {
            listeners.Remove(l);
        }

        public void Raise(T val)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Response(val);
            }
        }
    }
}