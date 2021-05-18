using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class Variable<T> : ScriptableObject
    {
        [SerializeField] protected T _value;

        public T value
        {
            get { return _value; }
            protected set
            {
                _value = value;
            }
        }

        public void Set(T v)
        {
            T prev = value;
            value = v;
        }

        public void Set(Variable<T> v)
        {
            Set(v.value);
        }
    }
}
