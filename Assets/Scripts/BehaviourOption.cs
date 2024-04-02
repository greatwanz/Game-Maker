using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public class BehaviourOption : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Prefab")]
        [SerializeField] private BehaviourParameterOption behaviourParameterOption;
        [Header("Reference")]
        [SerializeField] private UnityEngine.UI.Image _backgroundImage;
        [SerializeField] private UnityEngine.UI.Image _behaviourImage;
        [SerializeField] private UnityEngine.UI.Text _title;

        private Action<BehaviourOption> _removeAction;

        private EntityBehaviourData _entityBehaviourData;

        private List<BehaviourParameterOption> _prefabEntityBehaviourParameters = new List<BehaviourParameterOption>();
        
        public EntityBehaviourData EntityBehaviourData => _entityBehaviourData;

        public void Setup(EntityBehaviourData behaviourData, Action<BehaviourOption> removeAction)
        {
            _entityBehaviourData = behaviourData;
            _removeAction = removeAction;
            _behaviourImage.sprite = behaviourData.BehaviourOptionType.thumbnail;
            _title.text = behaviourData.BehaviourOptionType.optionName;
            
            foreach (var p in behaviourData.EntityParamValues)
            {
                var param = Instantiate(behaviourParameterOption, transform);
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

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _removeAction.Invoke(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _backgroundImage.color = Color.cyan;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _backgroundImage.color = Color.white;
        }
    }
}