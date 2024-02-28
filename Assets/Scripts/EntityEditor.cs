using System;
using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class EntityEditor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _buttonRoot;
        [SerializeField] private NonDraggableScrollRect _entitiesScrollView;
        [Header("Prefab")]
        [SerializeField] private EditorOption _entityOptionPrefab;
        [SerializeField] private EditorPanelButton _editorPanelButton;
        [Header("Definitions")]
        [SerializeField] private EditorPanelType[] _editorPanelTypes;
        [Header("Data")]
        [SerializeField] private EditorPanelTypeVariable _editorPanelTypeVariable;
        [SerializeField] private EditorPanelType _prefabPanelType;

        private List<EditorOption> _editorOptions = new List<EditorOption>();

        void Start()
        {
            foreach (var panelType in _editorPanelTypes)
            {
                foreach (var option in panelType._entityOptionTypes)
                {
                    EditorOption e = Instantiate(_entityOptionPrefab, _entitiesScrollView.content);
                    e.Setup(option, panelType);
                    _editorOptions.Add(e);
                }

                EditorPanelButton button = Instantiate(_editorPanelButton, _buttonRoot);
                button.Setup(panelType._panelName);
                button.Bind(new UnityEngine.Events.UnityAction(() => SwitchPanelToType(panelType)));
            }

            SwitchPanelToType(_editorPanelTypes[0]);
        }

        void SwitchPanelToType(EditorPanelType panelType)
        {
            _editorPanelTypeVariable.Set(panelType);
            foreach (var e in _editorOptions)
            {
                e.gameObject.SetActive(panelType == e.panelType);
            }
        }

        public void SaveEntity(Entity entity)
        {
            var entityData = entity.GetEntityData();
            EditorOption option = Instantiate(_entityOptionPrefab, _entitiesScrollView.content);
            var metadata = option.gameObject.AddComponent<PrefabMetaData>();
            metadata.entityData = entityData;
            option.Setup(entityData.entityType, _prefabPanelType);
            option.panelType = _prefabPanelType;
            _editorOptions.Add(option);
            option.gameObject.SetActive(_editorPanelTypeVariable.value == _prefabPanelType);
        }

        public void OnPlaymodeToggle(bool isPlaying)
        {
            _buttonRoot.gameObject.SetActive(!isPlaying);
            _entitiesScrollView.gameObject.SetActive(!isPlaying);
        }
    }
}
