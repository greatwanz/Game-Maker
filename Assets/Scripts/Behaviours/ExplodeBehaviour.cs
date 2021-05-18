using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class ExplodeBehaviour : EntityBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private ExplodeEffect _explodeEffect;

        public override void Execute(Entity e)
        {
            var explodeEffect = Instantiate(_explodeEffect);
            explodeEffect.transform.position = e.transform.position;
            explodeEffect.SetMesh(e.meshFilter.mesh);
            explodeEffect.Activate();
            Destroy(e.gameObject);
        }

        public override void Setup(Entity e)
        {
        }
    }
}
