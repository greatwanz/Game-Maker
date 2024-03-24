using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class ExplodeBehaviour : EntityBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private ExplodeEffect _explodeEffect;

        public override void Execute(Entity e, Dictionary<string, object> paramValues)
        {
            var explodeEffect = Instantiate(_explodeEffect);
            explodeEffect.transform.position = e.transform.position;
            explodeEffect.SetMesh(e.MeshFilter.mesh);
            explodeEffect.Activate();
            Destroy(e.gameObject);
        }

        protected override Dictionary<string, object> GetDefaultParameters()
        {
            return new Dictionary<string, object>();
        }
    }
}
