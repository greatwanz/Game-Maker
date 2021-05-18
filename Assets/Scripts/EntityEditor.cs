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

        private List<EditorOption> _editorOptions;

        void Start()
        {
            _editorOptions = new List<EditorOption>();

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
            foreach (var e in _editorOptions)
            {
                e.gameObject.SetActive(panelType == e.panelType);
            }
        }
    }
}
