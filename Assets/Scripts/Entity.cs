using System;
using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public struct EntityBehaviourData
    {
        public EntityBehaviour Behaviour;
        public Dictionary<string, object> EntityParamValues;

        public EntityBehaviourData(EntityBehaviour behaviour, Dictionary<string, object> paramValues)
        {
            Behaviour = behaviour;
            EntityParamValues = new Dictionary<string, object>();

            if (paramValues != null)
            {
                foreach (var p in paramValues)
                {
                    EntityParamValues.Add(p.Key, p.Value);
                }
            }
        }

        public void Execute(Entity e, Dictionary<string, object> paramValues)
        {
            Behaviour.Execute(e, paramValues);
        }

        public void SetParameters(Dictionary<string, object> paramValues)
        {
            EntityParamValues = paramValues;
        }
    }

    public class Entity : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private MeshFilter _meshFilter;
        [Header("Game Event")]
        [SerializeField] private EntityGameEvent _saveEntityEvent;

        private bool _canTrigger;

        private EntityType _entityType;
        
        private readonly List<EntityBehaviourData> _entityBehaviourData = new List<EntityBehaviourData>();
        
        public MeshFilter MeshFilter => _meshFilter;
        public EntityType EntityType => _entityType;
        public List<EntityBehaviourData> EntityBehaviourData => _entityBehaviourData;

        public void Setup(EntityType entityType)
        {
            _entityType = entityType;
            MeshFilter.mesh = entityType.mesh;
        }

        public void AddBehaviour(params EntityBehaviourData[] behaviours)
        {
            foreach (var b in behaviours)
            {
                _entityBehaviourData.Add(b);
            }
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

        private void ExecuteBehaviours()
        {
            foreach (var behaviourData in _entityBehaviourData)
            {
                behaviourData.Execute(this, behaviourData.EntityParamValues);
            }
        }

        public void OnPlaymodeToggle(bool isPlaying)
        {
            _canTrigger = isPlaying;
        }
    }
}
