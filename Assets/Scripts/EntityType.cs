using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EntityType : EditorOptionType
    {
        [Header("Reference")]
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

        }

        public override void OnDrop(Vector3 position)
        {
            Entity e = Instantiate(_entity);
            Setup(e);
            if (_prefabMetadata != null)
            {
                e.AddBehaviour(_prefabMetadata.entityData.entityBehaviours);
            }

            e.transform.position = position;
        }

        public override void Setup(Entity e)
        {
            e.meshFilter.mesh = _mesh;
            e.EntityType = this;
        }

        public override bool HasMesh()
        {
            return true;
        }
    }
}
