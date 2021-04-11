using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EditorOptions : MonoBehaviour
    {
        [SerializeField] private ScrollRect entitiesScrollView;
        [SerializeField] private ScrollRect behavioursScrollView;
        [Space]
        [SerializeField] private Entity entityPrefab;
        [SerializeField] private EntityData[] entityData;
        [SerializeField] private Behaviour[] behaviours;

        void Start()
        {
            foreach (var data in entityData)
            {
                Entity e = Instantiate(entityPrefab, transform);
                e.Setup(data);
            }
        }
    }
}