using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EditorOption : MonoBehaviour
    {
        [SerializeField] private PointerDownHandler pointerDownHandler;

        [SerializeField] private Image optionThumbnail;
        [SerializeField] private Text optionName;

        public void Setup(IEditorOption data)
        {
            optionThumbnail.sprite = data.thumbnail;
            optionName.text = data.optionName;
            pointerDownHandler.OnPointerDownEvent.AddListener(data.OnPointerDown);
        }
    }
}
