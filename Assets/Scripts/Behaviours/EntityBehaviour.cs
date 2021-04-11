using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public abstract class EntityBehaviour : ScriptableObject, IEditorOption
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

        public abstract void Execute(Entity e);

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnEnterOptionArea()
        {
        }

        public void OnLeaveOptionArea()
        {
        }

        public void OnDrop()
        {
        }
    }
}