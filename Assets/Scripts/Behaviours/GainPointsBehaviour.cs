using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class GainPointsBehaviour : EntityBehaviour
    {
        [Header("Definition")]
        [SerializeField] private int _pointsToGain;
        [Header("Game Events")]
        [SerializeField] private IntGameEvent _onGainPointsEvent;

        public override void Execute(Entity e, Dictionary<string, object> paramValues)
        {
            _onGainPointsEvent.Raise((int) paramValues[nameof(_pointsToGain)]);
        }

        protected override Dictionary<string, object> GetDefaultParameters()
        {
            return new Dictionary<string, object>()
            {
                {nameof(_pointsToGain), _pointsToGain}
            };
        }
    }
}
