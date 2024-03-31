using UnityEngine;
using UnityEngine.EventSystems;
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
        [Header("Prefab")]
        [SerializeField] private EditorOption _entityOptionPrefab;
        [SerializeField] private PrefabEditorOption _prefabEditorOptionPrefab;
        [SerializeField] private EditorPanelButton _editorPanelButton;
        [Header("Definitions")]
        [SerializeField] private EditorPanelType[] _editorPanelTypes;
        [Header("Data")]
        [SerializeField] private EditorPanelTypeVariable _editorPanelTypeVariable;
        [SerializeField] private EditorPanelType _prefabPanelType;
        [SerializeField] private EditorOptionSet _editorOptions;

        private Vector3 _entitiesScrollViewVisible;
        private Vector3 _entitiesScrollViewHidden;

        void Start()
        {
            _entitiesScrollViewVisible = _entitiesScrollView.transform.position;
            var width = _entitiesScrollView.GetComponent<RectTransform>().rect.width;
            _entitiesScrollViewHidden = new Vector3(_entitiesScrollViewVisible.x + width, _entitiesScrollViewVisible.y, _entitiesScrollViewVisible.z);

            foreach (var panelType in _editorPanelTypes)
            {
                foreach (var option in panelType.EntityOptionTypes)
                {
                    EditorOption e = Instantiate(_entityOptionPrefab, _entitiesScrollView.content);
                    e.Setup(option, panelType);
                    _editorOptions.Add(e);
                }

                EditorPanelButton button = Instantiate(_editorPanelButton, _buttonRoot);
                button.Setup(panelType.PanelName);
                button.Bind(() => SwitchPanelToType(panelType));
            }

            SwitchPanelToType(_editorPanelTypes[0]);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var result = Utility.GetEventSystemRaycastResults();

                foreach (var r in result)
                {
                    if (r.gameObject.GetComponent<ISelectHandler>() != null) return;
                }
                
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        void SwitchPanelToType(EditorPanelType panelType)
        {
            _editorPanelTypeVariable.Set(panelType);
            foreach (var e in _editorOptions.Items)
            {
                e.gameObject.SetActive(panelType == e.PanelType);
            }
        }

        public void SaveEntity(Entity entity)
        {
            PrefabEditorOption option = Instantiate(_prefabEditorOptionPrefab, _entitiesScrollView.content);
            option.Setup(entity.EntityType, _prefabPanelType);
            option.AddBehaviourData(entity.EntityBehaviourData);
            _editorOptions.Add(option);
            option.gameObject.SetActive(_editorPanelTypeVariable.Value == _prefabPanelType);
        }

        public void OnPlaymodeToggle(bool isPlaying)
        {
            _buttonRoot.gameObject.SetActive(!isPlaying);
            _entitiesScrollView.gameObject.SetActive(!isPlaying);
            _scoreboard.gameObject.SetActive(isPlaying);
        }

        public void OnToggleEditor(bool isOn)
        {
            _buttonRoot.gameObject.SetActive(isOn);
            _entitiesScrollView.gameObject.SetActive(isOn);
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
    }
}
