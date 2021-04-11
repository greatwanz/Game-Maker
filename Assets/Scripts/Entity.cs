using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        private List<Behaviour> entityBehaviours = new List<Behaviour>();

        public void Setup(EntityData data)
        {
            meshFilter.mesh = data.mesh;
        }

        public void AddBehaviour(Behaviour behaviour)
        {
            entityBehaviours.Add(behaviour);
        }

        public void RemoveBehaviour(Behaviour behaviour)
        {
            entityBehaviours.Remove(behaviour);
        }

        public void ExecuteBehaviours()
        {
            foreach (var behaviour in entityBehaviours)
            {
                behaviour.Execute(this);
            }
        }
    }
}