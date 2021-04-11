using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EntityData : ScriptableObject
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Sprite _thumbnail;

        public Mesh mesh
        {
            get => _mesh;
            private set { }
        }

        public Sprite thumbnail
        {
            get => _thumbnail;
            private set { }
        }
    }
}