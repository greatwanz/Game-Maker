using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public class PrefabEditorOption : EditorOption
    {
        [Header("Prefab")]
        [SerializeField] private BehaviourOption behaviourOptionPrefab;
        [Header("Reference")]
        [SerializeField] private Transform _entityBehaviourRootTransform;
        [Header("Data")]
        [SerializeField] private EntityBehaviourDataSet _currentBehaviourDataSet;
        [Header("Game Event")]
        [SerializeField] private EditorOptionSet _editorOptions;


        private readonly List<BehaviourOption> _prefabEntityBehaviours = new List<BehaviourOption>();

        public void AddBehaviourData(List<EntityBehaviourData> data)
        {
            foreach (var e in data)
            {
                var prefabEntityBehaviour = Instantiate(behaviourOptionPrefab, _entityBehaviourRootTransform);
                prefabEntityBehaviour.Setup(e, RemoveEntityBehaviour);
                _prefabEntityBehaviours.Add(prefabEntityBehaviour);
            } 
        }

        private void RemoveEntityBehaviour(BehaviourOption behaviourOption)
        {
            _prefabEntityBehaviours.Remove(behaviourOption);
            Destroy(behaviourOption.gameObject);
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

        public override void OnPointerDown(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Left)
            {
                _currentBehaviourDataSet.Add(GetBehaviourData());
                _background.color = Color.white;
                _onToggleEditorEvent.Raise(false);
                _onDragEditorOption.Raise(_editorOptionType);
            }
            else if (data.button == PointerEventData.InputButton.Right)
            {
                _editorOptions.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}