using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EditorOption : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Reference")]
        [SerializeField] protected Image _background;
        [SerializeField] protected Image _optionThumbnail;
        [SerializeField] protected Text _optionName;
        [Header("Game Event")]
        [SerializeField] protected EditorOptionGameEvent _onDragEditorOption;
        [SerializeField] protected BoolGameEvent _onToggleEditorEvent;

        protected EditorOptionType _editorOptionType;

        private EditorPanelType _panelType;
        public EditorPanelType PanelType => _panelType;

        public void Setup(string optionName, EditorOptionType optionType, EditorPanelType editorPanelType)
        {
            _optionThumbnail.sprite = optionType.thumbnail;
            _optionName.text = optionName;
            name = optionName;
            _panelType = editorPanelType;
            _editorOptionType = optionType;
        }
        
        public virtual void Setup(EntitySaveData data, EditorPanelType editorPanelType)
        {
            Setup(data.OptionType.optionName, data.OptionType, editorPanelType);
        }

        public virtual void OnPointerDown(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Left)
            {
                _background.color = Color.white;
                _onToggleEditorEvent.Raise(false);
                _onDragEditorOption.Raise(_editorOptionType);
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _background.color = Color.cyan;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _background.color = Color.white;
        }
    }
}
