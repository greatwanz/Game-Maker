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
        [SerializeField] private EntityGameEvent _saveEntityEvent;

        public MeshFilter meshFilter => _meshFilter;

        private readonly List<EntityBehaviour> entityBehaviours = new List<EntityBehaviour>();

        private bool _canTrigger;

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
            if (_canTrigger)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ExecuteBehaviours();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Destroy(gameObject);
                }
                else if (Input.GetMouseButtonDown(2))
                {
                    _saveEntityEvent.Raise(this);
                }
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

        public void OnPlaymodeToggle(bool isPlaying)
        {
            _canTrigger = isPlaying;
        }
    }
}
