using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EntityData : ScriptableObject, IEditorOption
    {
        [SerializeField] private string _optionName;
        [SerializeField] private Sprite _thumbnail;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Entity _entity;

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

        public Mesh mesh
        {
            get => _mesh;
            private set { }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Entity entity = Instantiate(_entity);
            entity.Setup(mesh);
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