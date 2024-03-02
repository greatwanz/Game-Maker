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
        [SerializeField] private EditorOptionGameEvent onDragEditorOption;

        private EditorOptionType _editorOptionType;

        public EditorPanelType panelType
        {
            get;
            set;
        }

        public void Setup(EditorOptionType optionType, EditorPanelType editorPanelType)
        {
            _optionThumbnail.sprite = optionType.thumbnail;
            _optionName.text = optionType.optionName;
            _pointerDownHandler.OnPointerDownEvent.AddListener(OnPointerDown);
            name = optionType.optionName;
            panelType = editorPanelType;
            _editorOptionType = optionType;
        }

        private void OnPointerDown(UnityEngine.EventSystems.PointerEventData data)
        {
            _editorOptionType.OnPointerDown(data);
            onDragEditorOption.Raise(_editorOptionType);
        }
    }
}
