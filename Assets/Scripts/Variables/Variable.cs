using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class Variable<T> : ScriptableObject
    {
        [System.NonSerialized] protected T _value;

        public T Value
        {
            get { return _value; }
            protected set
            {
                _value = value;
            }
        }

        public void Set(T v)
        {
            T prev = Value;
            Value = v;
        }
    }
}
