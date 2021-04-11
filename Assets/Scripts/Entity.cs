using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        private List<EntityBehaviour> entityBehaviours = new List<EntityBehaviour>();

        public void Setup(Mesh mesh)
        {
            meshFilter.mesh = mesh;
        }

        public void AddBehaviour(EntityBehaviour behaviour)
        {
            entityBehaviours.Add(behaviour);
        }

        public void RemoveBehaviour(EntityBehaviour behaviour)
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