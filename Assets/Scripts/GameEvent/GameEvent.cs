using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu(menuName = "GameEvent/Void Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

        public void Register(GameEventListener l)
        {
            _listeners.Add(l);
        }

        public void UnRegister(GameEventListener l)
        {
            _listeners.Remove(l);
        }

        public void Raise()
        {
            for (int i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].Response();
            }
        }
    }
}