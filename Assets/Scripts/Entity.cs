using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public class Entity : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [Header("Reference")]
        [SerializeField] private MeshFilter _meshFilter;
        [Header("Game Event")]
        [SerializeField] private EntityGameEvent _saveEntityEvent;
        [SerializeField] private EditorOptionGameEvent _dragEditorOptionEvent;
        [Header("Data")]
        [SerializeField] private EditorPanelTypeVariable _editorPanelTypeVariable;
        [SerializeField] private EditorPanelType _prefabEditorPanelType;

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
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canTrigger && eventData.button == PointerEventData.InputButton.Left)
            {
                ExecuteBehaviours();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) return;

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Destroy(gameObject);
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                _saveEntityEvent.Raise(this);
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_canTrigger && eventData.button == PointerEventData.InputButton.Left)
            {
                _dragEditorOptionEvent.Raise(_entityType);
                _meshFilter.gameObject.SetActive(false);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_canTrigger && eventData.button == PointerEventData.InputButton.Left)
            {
                if (!Utility.IsPointerOverUIElement())
                {
                    var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                    transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
                    _meshFilter.gameObject.SetActive(true);
                }
                else
                {
                    if (_editorPanelTypeVariable.value == _prefabEditorPanelType)
                    {
                        _saveEntityEvent.Raise(this);
                    }

                    Destroy(gameObject);
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}
