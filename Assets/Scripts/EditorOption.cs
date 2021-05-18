using System;
using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EditorOption : MonoBehaviour
    {
        [SerializeField] private PointerDownHandler _pointerDownHandler;

        [SerializeField] private Image _optionThumbnail;
        [SerializeField] private Text _optionName;

        public EditorPanelType panelType
        {
            get;
            set;
        }

        public void Setup(EditorOptionType data, EditorPanelType editorPanelType)
        {
            _optionThumbnail.sprite = data.thumbnail;
            _optionName.text = data.optionName;
            _pointerDownHandler.OnPointerDownEvent.AddListener(data.OnPointerDown);
            name = data.optionName;
            panelType = editorPanelType;
        }
    }
}
