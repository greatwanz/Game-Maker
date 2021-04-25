using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public abstract class EditorOptionType : ScriptableObject
    {
        [SerializeField] private string _optionName;
        [SerializeField] private Sprite _thumbnail;

        public string optionName
        {
            get => _optionName;
            private set { }
        }

        public Sprite thumbnail
        {
            get => _thumbnail;
            private set { }
        }

        public abstract void Setup(Entity e);

        public abstract void OnPointerDown(PointerEventData eventData);

        public abstract void OnDrop();
    }
}
