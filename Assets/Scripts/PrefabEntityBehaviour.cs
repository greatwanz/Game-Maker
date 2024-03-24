
using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class PrefabEntityBehaviour : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private PrefabEntityBehaviourParameter prefabEntityBehaviourParameter;
        [Header("Reference")]
        [SerializeField] private UnityEngine.UI.Image _image;
        [SerializeField] private UnityEngine.UI.Text _title;

        private EntityBehaviourData _entityBehaviourData;

        private List<PrefabEntityBehaviourParameter> _prefabEntityBehaviourParameters = new List<PrefabEntityBehaviourParameter>();
        
        public EntityBehaviourData EntityBehaviourData => _entityBehaviourData;

        public void Setup(EntityBehaviourData behaviourData)
        {
            _entityBehaviourData = behaviourData;
            _image.sprite = behaviourData.Behaviour.thumbnail;
            _title.text = behaviourData.Behaviour.optionName;
            
            foreach (var p in behaviourData.EntityParamValues)
            {
                var param = Instantiate(prefabEntityBehaviourParameter, transform);
                param.Setup(p.Key, p.Value);
                _prefabEntityBehaviourParameters.Add(param);
            }
        }

        public void SetParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (var p in _prefabEntityBehaviourParameters)
            {
                p.GetParameterValues(ref parameters);
            }
            
            _entityBehaviourData.SetParameters(parameters);
        }
    }
}