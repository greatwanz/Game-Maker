using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EntityEditor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _buttonRoot;
        [SerializeField] private NonDraggableScrollRect _entitiesScrollView;
        [SerializeField] private Scoreboard _scoreboard;
        [SerializeField] private ModeToggle _modeToggle;
        [SerializeField] private Text _panelVisibilityIndicator;
        [SerializeField] private Toggle _panelVisibilityIndicatorButton;
        [Header("Prefab")]
        [SerializeField] private EditorOption _entityOptionPrefab;
        [SerializeField] private PrefabEditorOption _prefabEditorOptionPrefab;
        [SerializeField] private EditorPanelButton _editorPanelButtonPrefab;
        [SerializeField] private BehaviourOption _behaviourOptionPrefab;
        [Header("Definitions")]
        [SerializeField] private EditorPanelType[] _editorPanelTypes;
        [Header("Data")]
        [SerializeField] private EditorPanelTypeVariable _editorPanelTypeVariable;
        [SerializeField] private EntityVariable _currentEntityVariable;
        [SerializeField] private EditorPanelType _prefabPanelType;
        [SerializeField] private EditorPanelType _inspectorPanelType;
        [SerializeField] private EditorOptionSet _editorOptionSet;

        private Vector3 _entitiesScrollViewVisible;
        private Vector3 _entitiesScrollViewHidden;

        private List<EditorPanelButton> _editorPanelButtons;

        private EditorPanelType _panelTypeBeforeInspector;

        void Start()
        {
            _editorPanelButtons = new List<EditorPanelButton>();
            _entitiesScrollViewVisible = _entitiesScrollView.transform.position;
            var width = _entitiesScrollView.GetComponent<RectTransform>().rect.width;
            _entitiesScrollViewHidden = new Vector3(_entitiesScrollViewVisible.x + width, _entitiesScrollViewVisible.y, _entitiesScrollViewVisible.z);

            foreach (var panelType in _editorPanelTypes)
            {
                foreach (var option in panelType.EntityOptionTypes)
                {
                    EditorOption e = Instantiate(_entityOptionPrefab, _entitiesScrollView.content);
                    e.Setup(option.optionName, option, panelType);
                    _editorOptionSet.Add(e);
                }

                EditorPanelButton button = Instantiate(_editorPanelButtonPrefab, _buttonRoot);
                button.Setup(panelType);
                button.Bind(() => SwitchPanelToType(panelType));
                _editorPanelButtons.Add(button);
                if(!panelType.IsDefaultEnabled) button.gameObject.SetActive(false);
            }

            SwitchPanelToType(_editorPanelTypes[0]);
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame || Mouse.current.rightButton.wasReleasedThisFrame)
            {
                if (Utility.IsPointerOverUIElement()) return;
                var result = Utility.GetEventSystemRaycastResults();
                foreach (var r in result)
                {
                    var e = r.gameObject.GetComponent<Entity>();
                    if (e != null) return;
                }
                
                DeselectCurrentEntity();
            }
        }

        void SwitchPanelToType(EditorPanelType panelType)
        {
            _editorPanelTypeVariable.Set(panelType);
            foreach (var e in _editorOptionSet.Items)
            {
                e.gameObject.SetActive(panelType == e.PanelType);
            }
        }

        public void OnSaveEntity(Entity entity)
        {
            PrefabEditorOption option = Instantiate(_prefabEditorOptionPrefab, _entitiesScrollView.content);
            option.Setup(entity.EntityName, entity.EntityOptionType, _prefabPanelType);
            option.AddBehaviourData(entity.EntityBehaviourData);
            _editorOptionSet.Add(option);
            option.gameObject.SetActive(_editorPanelTypeVariable.Value == _prefabPanelType);
        }

        public void OnPlaymodeToggle(bool isPlaying)
        {
            _buttonRoot.gameObject.SetActive(!isPlaying);
            _entitiesScrollView.gameObject.SetActive(!isPlaying);
            _scoreboard.gameObject.SetActive(isPlaying);

            if (isPlaying)
            {
                DeselectCurrentEntity();
            }
        }

        private bool _isEditorOnPreDrag = true;

        public void OnToggleEditor(bool isOn)
        {
            _panelVisibilityIndicatorButton.gameObject.SetActive(isOn);
            if (_currentEntityVariable.Value)
            {
                if (!isOn)
                {
                    _isEditorOnPreDrag = _modeToggle.gameObject.activeSelf;
                }
                else
                {
                    isOn = _isEditorOnPreDrag;
                }
            }
            
            _entitiesScrollView.transform.position = isOn ? _entitiesScrollViewVisible : _entitiesScrollViewHidden;
            _buttonRoot.gameObject.SetActive(isOn);
            _scoreboard.gameObject.SetActive(isOn);
            _modeToggle.gameObject.SetActive(isOn);
        }

        public void OnToggleCollapseEditor(bool isVisible)
        {
            _entitiesScrollView.transform.position = isVisible ? _entitiesScrollViewVisible : _entitiesScrollViewHidden;
            _buttonRoot.gameObject.SetActive(isVisible);
            
            _scoreboard.gameObject.SetActive(isVisible);
            _modeToggle.gameObject.SetActive(isVisible);

            _panelVisibilityIndicator.text = isVisible ? ">" : "<";
        }

        public void OnEntitySelected(Entity entity)
        {
            if (entity)
            {
                _panelTypeBeforeInspector = _editorPanelTypeVariable.Value;
                SwitchPanelToType(_inspectorPanelType);
                foreach (var e in _editorPanelButtons)
                {
                    if (e.PanelType == _inspectorPanelType)
                    {
                        e.SetPanelButtonName(entity.EntityName);
                        e.gameObject.SetActive(true);
                    }
                    else
                    {
                        e.gameObject.SetActive(false);
                    }
                }

                foreach (var e in entity.EntityBehaviourData)
                {
                    BehaviourOption option = Instantiate(_behaviourOptionPrefab,  _entitiesScrollView.content);
                    option.Setup(e, option =>
                    {
                        entity.EntityBehaviourData.Remove(e);
                        Destroy(option.gameObject);
                    });
                }
            }
            else
            {
                if (_editorPanelTypeVariable.Value == _inspectorPanelType)
                {
                    foreach (Transform t in _entitiesScrollView.content)
                    {
                        if (t.gameObject.activeSelf) Destroy(t.gameObject);
                    }

                    foreach (var e in _editorPanelButtons)
                    {
                        e.gameObject.SetActive(e.PanelType != _inspectorPanelType);
                    }

                    SwitchPanelToType(_panelTypeBeforeInspector);
                }
            }
        }

        private void DeselectCurrentEntity()
        {
            if (Mouse.current.leftButton.isPressed) return;

            EventSystem.current.SetSelectedGameObject(null);
            if (_currentEntityVariable.Value)
            {
                _currentEntityVariable.Value.Deselect();
                _currentEntityVariable.Set(null);
            }
        }
    }
}
