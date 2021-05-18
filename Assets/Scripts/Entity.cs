using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;

        public MeshFilter meshFilter => _meshFilter;

        [SerializeField] private readonly List<EntityBehaviour> entityBehaviours = new List<EntityBehaviour>();

        public void AddBehaviour(EntityBehaviour behaviour)
        {
            if (!entityBehaviours.Contains(behaviour))
            {
                entityBehaviours.Add(behaviour);
            }
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
