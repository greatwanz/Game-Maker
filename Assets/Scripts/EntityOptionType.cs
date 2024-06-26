using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EntityOptionType : EditorOptionType
    {
        [Header("Reference")]
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Entity _entity;

        public Mesh Mesh => _mesh;

        public override Entity OnDrop(Vector3 position)
        {
            Entity e = Instantiate(_entity);
            e.Setup(this, OptionName);
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
