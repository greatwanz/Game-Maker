using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EntityType : EditorOptionType
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Entity _entity;
        [SerializeField] private EditorOptionGameEvent onDragEditorOption;

        public Mesh mesh
        {
            get => _mesh;
            private set { }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            onDragEditorOption.Raise(this);
        }

        public override void OnDrop()
        {
            Entity entity = Instantiate(_entity);
            Setup(entity);

            //Current location of mouse pointer and distance in front of camera
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
            //Convert from screen space to world space
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            entity.transform.position = new Vector3(curPosition.x, curPosition.y, entity.transform.position.z);
        }

        public override void Setup(Entity e)
        {
            e.meshFilter.mesh = mesh;
        }
    }
}
