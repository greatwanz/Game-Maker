using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class PrefabEditorOption : EditorOption
    {
        [Header("Prefab")]
        [SerializeField] private PrefabEntityBehaviour prefabEntityBehaviourPrefab;
        [Header("Reference")]
        [SerializeField] private Transform _entityBehaviourRootTransform;

        private readonly List<PrefabEntityBehaviour> _prefabEntityBehaviours = new List<PrefabEntityBehaviour>();

        public void AddBehaviourData(List<EntityBehaviourData> data)
        {
            foreach (var e in data)
            {
                var prefabEntityBehaviour = Instantiate(prefabEntityBehaviourPrefab, _entityBehaviourRootTransform);
                prefabEntityBehaviour.Setup(e);
                _prefabEntityBehaviours.Add(prefabEntityBehaviour);
            } 
        }

        public List<EntityBehaviourData> GetBehaviourData()
        {
            List<EntityBehaviourData> behaviours = new List<EntityBehaviourData>();
            foreach (var p in _prefabEntityBehaviours)
            {
                p.SetParameters();
                behaviours.Add(p.EntityBehaviourData);
            }

            return behaviours;
        }
    }
}