using System;
using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class EntityEditor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform buttonRoot;
        [SerializeField] private NonDraggableScrollRect entitiesScrollView;
        [Header("Prefab")]
        [SerializeField] private EditorOption entityOptionPrefab;
        [SerializeField] private EditorPanelButton editorPanelButton;
        [Header("Definitions")]
        [SerializeField] private EditorPanelType[] editorPanelTypes;

        private List<EditorOption> editorOptions;

        void Start()
        {
            editorOptions = new List<EditorOption>();

            foreach (var panelType in editorPanelTypes)
            {
                foreach (var option in panelType.entityOptionTypes)
                {
                    EditorOption e = Instantiate(entityOptionPrefab, entitiesScrollView.content);
                    e.Setup(option, panelType);
                    editorOptions.Add(e);
                }

                EditorPanelButton button = Instantiate(editorPanelButton, buttonRoot);
                button.Setup(panelType.panelName);
                button.Bind(new UnityEngine.Events.UnityAction(() => SwitchPanelToType(panelType)));
            }

            SwitchPanelToType(editorPanelTypes[0]);
        }

        void SwitchPanelToType(EditorPanelType panelType)
        {
            foreach (var e in editorOptions)
            {
                e.gameObject.SetActive(panelType == e.panelType);
            }
        }
    }
}
