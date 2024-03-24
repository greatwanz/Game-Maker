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
        [SerializeField] private PrefabEditorOption _prefabEditorOptionPrefab;
        [SerializeField] private EditorPanelButton _editorPanelButton;
        [Header("Definitions")]
        [SerializeField] private EditorPanelType[] _editorPanelTypes;
        [Header("Data")]
        [SerializeField] private EditorPanelTypeVariable _editorPanelTypeVariable;
        [SerializeField] private EditorPanelType _prefabPanelType;

        private readonly List<EditorOption> _editorOptions = new List<EditorOption>();

        void Start()
        {
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

        void SwitchPanelToType(EditorPanelType panelType)
        {
            _editorPanelTypeVariable.Set(panelType);
            foreach (var e in _editorOptions)
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
            option.gameObject.SetActive(_editorPanelTypeVariable.value == _prefabPanelType);
        }

        public void OnPlaymodeToggle(bool isPlaying)
        {
            _buttonRoot.gameObject.SetActive(!isPlaying);
            _entitiesScrollView.gameObject.SetActive(!isPlaying);
        }
    }
}
