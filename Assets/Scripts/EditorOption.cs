using System;
using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EditorOption : MonoBehaviour
    {
        [SerializeField] private PointerDownHandler pointerDownHandler;

        [SerializeField] private Image optionThumbnail;
        [SerializeField] private Text optionName;

        public EditorPanelType panelType
        {
            get;
            set;
        }

        public void Setup(EditorOptionType data, EditorPanelType editorPanelType)
        {
            optionThumbnail.sprite = data.thumbnail;
            optionName.text = data.optionName;
            pointerDownHandler.OnPointerDownEvent.AddListener(data.OnPointerDown);
            name = data.optionName;
            panelType = editorPanelType;
        }
    }
}
