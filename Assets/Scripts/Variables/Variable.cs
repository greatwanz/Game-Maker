using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class Variable<T> : ScriptableObject
    {
        [System.NonSerialized] private T _value;

        public T Value
        {
            get => _value;
            private set => _value = value;
        }

        public void Set(T v)
        {
            T prev = Value;
            Value = v;
        }
    }
}
