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
        [Header("Data")]
        [SerializeField] protected ColourSettings _colourSettings;
        [Header("Game Event")]
        [SerializeField] protected EditorOptionGameEvent _onDragEditorOption;
        [SerializeField] protected BoolGameEvent _onToggleEditorEvent;

        private EditorPanelType _panelType;
        
        protected EditorOptionType _editorOptionType;

        public EditorPanelType PanelType => _panelType;

        public void Setup(string optionName, EditorOptionType optionType, EditorPanelType editorPanelType)
        {
            _optionThumbnail.sprite = optionType.Thumbnail;
            _optionName.text = optionName;
            name = optionName;
            _panelType = editorPanelType;
            _editorOptionType = optionType;
        }
        
        public virtual void Setup(EntitySaveData data, EditorPanelType editorPanelType)
        {
            Setup(data.Name, data.OptionType, editorPanelType);
        }

        public virtual void OnPointerDown(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Left)
            {
                _background.color = _colourSettings.DefaultColour;
                _onToggleEditorEvent.Raise(false);
                _onDragEditorOption.Raise(_editorOptionType);
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _background.color = _colourSettings.SelectedColour;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _background.color = _colourSettings.DefaultColour;
        }
    }
}
