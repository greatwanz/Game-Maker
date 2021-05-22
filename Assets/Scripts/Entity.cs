using System;
using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    [Serializable]
    public struct EntityData
    {
        public EntityType entityType { get; }
        public IEnumerable<EntityBehaviour> entityBehaviours { get; }

        public EntityData(EntityType type, List<EntityBehaviour> behaviours)
        {
            entityType = type;
            entityBehaviours = behaviours;
        }
    }

    public class Entity : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;

        public MeshFilter meshFilter => _meshFilter;

        [SerializeField] private readonly List<EntityBehaviour> entityBehaviours = new List<EntityBehaviour>();

        public EntityType entityType
        {
            get;
            set;
        }

        public void AddBehaviour(EntityBehaviour behaviour)
        {
            if (!entityBehaviours.Contains(behaviour))
            {
                entityBehaviours.Add(behaviour);
            }
        }

        public void AddBehaviour(IEnumerable<EntityBehaviour> behaviours)
        {
            foreach (var b in behaviours)
            {
                AddBehaviour(b);
            }
        }

        public void RemoveBehaviour(EntityBehaviour behaviour)
        {
            entityBehaviours.Remove(behaviour);
        }

        public void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ExecuteBehaviours();
            }
        }

        public void ExecuteBehaviours()
        {
            foreach (var behaviour in entityBehaviours)
            {
                behaviour.Execute(this);
            }
        }

        public EntityData GetEntityData()
        {
            return new EntityData(entityType, entityBehaviours);
        }
    }
}
