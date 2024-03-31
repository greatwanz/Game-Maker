using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu(menuName = "Variables/Integer")]
    public class IntVariable : Variable<int>
    {
        public void Add(int v)
        {
            int prev = Value;
            prev += v;
            Set(prev);
        }

        public void Add(IntVariable v)
        {
            int prev = Value;

            if (v is IntVariable)
            {
                IntVariable i = (IntVariable)v;
                prev += i.Value;
                Set(prev);
            }
        }

        public static IntVariable operator ++(IntVariable c1)
        {
            c1.Add(1);
            return c1;
        }

        public static IntVariable operator --(IntVariable c1)
        {
            c1.Add(-1);
            return c1;
        }
    }
}

