using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class GainPointsBehaviour : EntityBehaviour
    {
        [Header("Definition")]
        [SerializeField] private int _pointsToGain;
        [Header("Game Events")]
        [SerializeField] private IntGameEvent _gainPointsEvent;

        public override void Execute(Entity e)
        {
            _gainPointsEvent.Raise(_pointsToGain);
        }
    }
}
