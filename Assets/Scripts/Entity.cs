using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;

        public MeshFilter meshFilter
        {
            get { return _meshFilter; }
            private set { _meshFilter = value; }
        }

        private List<EntityBehaviour> entityBehaviours = new List<EntityBehaviour>();

        public void AddBehaviour(EntityBehaviour behaviour)
        {
            entityBehaviours.Add(behaviour);
        }

        public void RemoveBehaviour(EntityBehaviour behaviour)
        {
            entityBehaviours.Remove(behaviour);
        }

        public void OnMouseDown()
        {
            ExecuteBehaviours();
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
