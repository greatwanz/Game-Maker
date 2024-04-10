using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class BehaviourOption : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Prefab")]
        [SerializeField] private BehaviourOptionParameter _behaviourOptionParameterPrefab;
        [Header("Data")]
        [SerializeField] private ColourSettings _colourSettings;
        [Header("Reference")]
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _behaviourImage;
        [SerializeField] private Text _title;

        private Action<BehaviourOption> _removeAction;

        private EntityBehaviourData _entityBehaviourData;

        private readonly List<BehaviourOptionParameter> _prefabEntityBehaviourParameters = new List<BehaviourOptionParameter>();
        
        public EntityBehaviourData EntityBehaviourData => _entityBehaviourData;

        public void Setup(EntityBehaviourData behaviourData, Action<BehaviourOption> removeAction)
        {
            _entityBehaviourData = behaviourData;
            _removeAction = removeAction;
            _behaviourImage.sprite = behaviourData.BehaviourOptionType.Thumbnail;
            _title.text = behaviourData.BehaviourOptionType.OptionName;
            
            foreach (var p in behaviourData.EntityParamValues)
            {
                var param = Instantiate(_behaviourOptionParameterPrefab, transform);
                param.Setup(this, p.Key, p.Value);
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
            _backgroundImage.color = _colourSettings.SelectedColour;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _backgroundImage.color = _colourSettings.DefaultColour;
        }
    }
}