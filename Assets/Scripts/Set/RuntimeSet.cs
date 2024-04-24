using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class RuntimeSet<T> : ScriptableObject
    {
        private readonly List<T> _items = new List<T>();
        
        public List<T> Items => _items;

        public void Add(T item)
        {
            if(!_items.Contains(item)) _items.Add(item);
        }
        
        public void Add(List<T> items)
        {
            foreach (var i in items)
            {
                if(!_items.Contains(i)) _items.Add(i);
            }
        }
        
        public void Remove(T item)
        {
            if(_items.Contains(item)) _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}