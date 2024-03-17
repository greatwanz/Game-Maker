using System;
using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EditorOption : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private Image _optionThumbnail;
        [SerializeField] private Text _optionName;
        [Header("Game Event")]
        [SerializeField] private PointerDownHandler _pointerDownHandler;
        [SerializeField] private EditorOptionGameEvent _onDragEditorOption;

        private EditorOptionType _editorOptionType;

        private EditorPanelType _panelType;
        public EditorPanelType PanelType => _panelType;

        private void OnDestroy()
        {
            _pointerDownHandler.OnPointerDownEvent.RemoveAllListeners();
        }

        public void Setup(EditorOptionType optionType, EditorPanelType editorPanelType)
        {
            _optionThumbnail.sprite = optionType.thumbnail;
            _optionName.text = optionType.optionName;
            _pointerDownHandler.OnPointerDownEvent.AddListener(OnPointerDown);
            name = optionType.optionName;
            _panelType = editorPanelType;
            _editorOptionType = optionType;
        }

        private void OnPointerDown(UnityEngine.EventSystems.PointerEventData data)
        {
            _editorOptionType.OnPointerDown(data);
            _onDragEditorOption.Raise(_editorOptionType);
        }
    }
}
