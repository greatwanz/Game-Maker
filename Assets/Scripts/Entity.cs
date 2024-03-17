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
        [Header("Reference")]
        [SerializeField] private MeshFilter _meshFilter;
        [Header("Game Event")]
        [SerializeField] private EntityGameEvent _saveEntityEvent;

        public MeshFilter meshFilter => _meshFilter;

        private readonly List<EntityBehaviour> _entityBehaviours = new List<EntityBehaviour>();

        private bool _canTrigger;

        private EntityType _entityType;
        public EntityType EntityType => _entityType;

        public void Setup(EntityType entityType)
        {
            _entityType = entityType;
            meshFilter.mesh = entityType.mesh;
        }

        public void AddBehaviour(EntityBehaviour behaviour)
        {
            if (!_entityBehaviours.Contains(behaviour))
            {
                _entityBehaviours.Add(behaviour);
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
            _entityBehaviours.Remove(behaviour);
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
                if (Input.GetMouseButton(0)) return;

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
            foreach (var behaviour in _entityBehaviours)
            {
                behaviour.Execute(this);
            }
        }

        public EntityData GetEntityData()
        {
            return new EntityData(EntityType, _entityBehaviours);
        }

        public void OnPlaymodeToggle(bool isPlaying)
        {
            _canTrigger = isPlaying;
        }
    }
}
