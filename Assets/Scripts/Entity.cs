using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Greatwanz.GameMaker
{
    public struct EntityBehaviourData
    {
        public BehaviourOptionType BehaviourOptionType;
        public Dictionary<string, object> EntityParamValues;

        public EntityBehaviourData(BehaviourOptionType behaviourOptionType, Dictionary<string, object> paramValues)
        {
            BehaviourOptionType = behaviourOptionType;
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
            BehaviourOptionType.Execute(e, paramValues);
        }

        public void SetParameter(string key, object value)
        {
            if (EntityParamValues.ContainsKey(key))
            {
                EntityParamValues[key] = value;
            }
        }

        public void SetParameters(Dictionary<string, object> paramValues)
        {
            EntityParamValues = paramValues;
        }
    }

    public struct EntitySaveData
    {
        public string Name;
        public Vector3 Position;
        public EntityOptionType OptionType;
        public readonly List<EntityBehaviourData> EntityBehaviourData;

        public EntitySaveData(string name, Vector3 position, EntityOptionType optionType, List<EntityBehaviourData> entityBehaviourData)
        {
            Name = name;
            Position = position;
            OptionType = optionType;
            EntityBehaviourData = new List<EntityBehaviourData>();
            
            foreach (var e in entityBehaviourData)
            {
                var paramValues = new Dictionary<string, object>();
                foreach (var p in e.EntityParamValues)
                {
                    paramValues.Add(p.Key, p.Value);
                }

                EntityBehaviourData data = new EntityBehaviourData(e.BehaviourOptionType, paramValues);
                EntityBehaviourData.Add(data);
            }
        }

        public void SetupEntityFromSaveData(Entity entity)
        {
            entity.transform.position = Position;
            entity.Setup(OptionType, Name);
            entity.AddBehaviour(EntityBehaviourData.ToArray());
        }
    }

    public class Entity : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, 
        IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private bool _isInteractable = true;
        [Header("Reference")]
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        [Header("Data")]
        [SerializeField] private EntityVariable _currentEntityVariable;
        [Header("Game Event")]
        [SerializeField] private EntityGameEvent _onSaveEntityEvent;
        [SerializeField] private EntityGameEvent _onEntitySelectedEvent;
        [SerializeField] private EditorOptionGameEvent _dragEditorOptionEvent;
        [SerializeField] private BoolGameEvent _onToggleEditorEvent;

        private bool _isDragging;
        private bool _canTrigger;
        private bool _isSelected;

        private Vector3 _preDragPosition;
        private Vector3 _curScreenPoint;

        private string _entityName;

        private EntityOptionType _entityOptionType;
        
        private readonly List<EntityBehaviourData> _entityBehaviourData = new List<EntityBehaviourData>();

        public MeshFilter MeshFilter => _meshFilter;
        public List<EntityBehaviourData> EntityBehaviourData => _entityBehaviourData;

        public string EntityName
        {
            set => _entityName = value;
            get => _entityName;
        }

        public void Setup(EntityOptionType entityOptionType, string entityName)
        {
            _entityOptionType = entityOptionType;
            _entityName = entityName;
            MeshFilter.mesh = entityOptionType.mesh;
        }

        public EntitySaveData CreateEntitySaveData()
        {
           return new EntitySaveData(EntityName, transform.position, _entityOptionType, _entityBehaviourData);
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
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            if (_canTrigger)
            {
                ExecuteBehaviours();
            }
            else
            {
                if(_isSelected) _isDragging = true;
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
                    if (Mouse.current.leftButton.isPressed) return;
                    Deselect();
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
                _onToggleEditorEvent.Raise(true);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isInteractable || !_isSelected || !_isDragging) return;
            
            if (Utility.IsPointerOverUIElement())
            {
                _dragEditorOptionEvent.Raise(_entityOptionType);
                _meshFilter.gameObject.SetActive(false);
            }
            else
            {
                var mousePosition = Mouse.current.position.ReadValue();
                _curScreenPoint = new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
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
            if (_currentEntityVariable.Value != null)
            {
                _currentEntityVariable.Value.Deselect();
            }
            
            _isSelected = true;
            _currentEntityVariable.Set(this);
            _onEntitySelectedEvent.Raise(this);
        }

        public void OnDeselect(BaseEventData eventData)
        {

        }

        public void Deselect()
        {
            _isSelected = false;
            _meshRenderer.material.SetColor("_Color", Color.white);
            _onEntitySelectedEvent.Raise(null); 
            _currentEntityVariable.Set(null);
        }
    }
}
