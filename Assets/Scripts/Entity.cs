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

    public class Entity : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, 
        IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private bool _isInteractable = true;
        [Header("Reference")]
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        [Header("Game Event")]
        [SerializeField] private EntityGameEvent _onSaveEntityEvent;
        [SerializeField] private EditorOptionGameEvent _dragEditorOptionEvent;
        [SerializeField] private BoolGameEvent _onToggleEditorEvent;

        private bool _isDragging;
        private bool _canTrigger;
        private bool _isSelected;

        private Vector3 _preDragPosition;
        private Vector3 _curScreenPoint;

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
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canTrigger && eventData.button == PointerEventData.InputButton.Left)
            {
                ExecuteBehaviours();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_canTrigger) return;

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (!_isSelected && eventData.hovered.Contains(gameObject))
                {
                    EventSystem.current.SetSelectedGameObject(gameObject);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_isDragging)
                {
                    _isInteractable = false;
                    transform.position = _preDragPosition;
                    _meshFilter.gameObject.SetActive(true);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                _onSaveEntityEvent.Raise(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_isSelected) return;
            if (!_canTrigger && eventData.button == PointerEventData.InputButton.Left)
            {
                _preDragPosition = transform.position;
                _isDragging = true;
                _onToggleEditorEvent.Raise(false);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isSelected) return;
            
            if (!_canTrigger && eventData.button == PointerEventData.InputButton.Left)
            {
                _isInteractable = true;
                _isDragging = false;
                Input.ResetInputAxes();
                _onToggleEditorEvent.Raise(true);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isInteractable || !_isSelected || !_isDragging) return;
            
            if (Utility.IsPointerOverUIElement())
            {
                _dragEditorOptionEvent.Raise(_entityType);
                _meshFilter.gameObject.SetActive(false);
            }
            else
            {
                _curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                transform.position = Camera.main.ScreenToWorldPoint(_curScreenPoint);
                _meshFilter.gameObject.SetActive(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isInteractable || _canTrigger) return;
            _meshRenderer.material.SetColor("_Color", Color.cyan);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isSelected || !_isInteractable || _canTrigger) return;
            _meshRenderer.material.SetColor("_Color", Color.white);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _isSelected = false;
            _meshRenderer.material.SetColor("_Color", Color.white);
        }
    }
}
