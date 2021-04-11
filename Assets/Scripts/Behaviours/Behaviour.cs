using UnityEngine;

namespace Greatwanz.GameMaker
{
    public abstract class Behaviour : ScriptableObject
    {
        public abstract void Execute(Entity e);
    }
}