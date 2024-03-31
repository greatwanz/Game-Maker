using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EntityType : EditorOptionType
    {
        [Header("Reference")]
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Entity _entity;

        public Mesh mesh => _mesh;

        public override Entity OnDrop(Vector3 position)
        {
            Entity e = Instantiate(_entity);
            e.Setup(this);
            e.transform.position = position;
            return e;
        }

        public void OnDrop(Vector3 position, List<EntityBehaviourData> entityBehaviourData)
        {
            Entity e = OnDrop(position);
            if (entityBehaviourData != null)
            {
                e.AddBehaviour(entityBehaviourData.ToArray());
            }
        }
    }
}
