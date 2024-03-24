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

        private PrefabEditorOption _prefabEditorOption;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.isValid)
            {
                var prefabEditorOption = eventData.pointerCurrentRaycast.gameObject.GetComponent<PrefabEditorOption>();
                if (prefabEditorOption)
                {
                    _prefabEditorOption = prefabEditorOption;
                }
            }

        }

        public override void OnDrop(Vector3 position)
        {
            Entity e = Instantiate(_entity);
            e.Setup(this);
            if (_prefabEditorOption != null)
            {
                e.AddBehaviour(_prefabEditorOption.GetBehaviourData().ToArray());
            }

            e.transform.position = position;
        }

        public override bool HasMesh()
        {
            return true;
        }
    }
}
