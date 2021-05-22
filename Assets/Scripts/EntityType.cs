using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EntityType : EditorOptionType
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Entity _entity;

        public Mesh mesh => _mesh;
        public Entity entity => _entity;

        private PrefabMetaData _prefabMetadata;

        public override void OnPointerDown(PointerEventData eventData)
        {
            _prefabMetadata = null;
            if (eventData.pointerCurrentRaycast.isValid)
            {
                var metadata = eventData.pointerCurrentRaycast.gameObject.GetComponent<PrefabMetaData>();
                if (metadata)
                {
                    _prefabMetadata = metadata;
                }
            }

            onDragEditorOption.Raise(this);
        }

        public override void OnDrop()
        {
            Entity e = Instantiate(entity);
            Setup(e);
            if (_prefabMetadata != null)
            {
                e.AddBehaviour(_prefabMetadata.entityData.entityBehaviours);
            }

            //Current location of mouse pointer and distance in front of camera
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
            //Convert from screen space to world space
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            e.transform.position = new Vector3(curPosition.x, curPosition.y, e.transform.position.z);
        }

        public override void Setup(Entity e)
        {
            e.meshFilter.mesh = mesh;
            e.entityType = this;
        }

        public override bool HasMesh()
        {
            return true;
        }
    }
}
