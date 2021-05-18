using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class ExplodeEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fireball;
        [SerializeField] private ParticleSystem _fragmentation;

        public void Activate()
        {
            _fragmentation.gameObject.SetActive(true);
            _fireball.gameObject.SetActive(true);
            float duration = _fireball.main.startDelay.constant + _fireball.main.startLifetime.constantMax +
                _fragmentation.main.startDelay.constant + _fragmentation.main.startLifetime.constantMax;
            Destroy(gameObject, duration);
        }

        public void SetMesh(Mesh mesh)
        {
            _fragmentation.GetComponent<ParticleSystemRenderer>().mesh = mesh;
            _fireball.GetComponent<ParticleSystemRenderer>().mesh = mesh;
        }
    }
}
