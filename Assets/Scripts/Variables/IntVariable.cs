using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu(menuName = "Variables/Integer")]
    public class IntVariable : Variable<int>
    {
        public void Add(int v)
        {
            int prev = value;
            prev += v;
            Set(prev);
        }

        public void Add(IntVariable v)
        {
            int prev = value;

            if (v is IntVariable)
            {
                IntVariable i = (IntVariable)v;
                prev += i.value;
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

