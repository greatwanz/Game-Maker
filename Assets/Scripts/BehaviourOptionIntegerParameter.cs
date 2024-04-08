using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class BehaviourOptionIntegerParameter : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private Text _parameterName;
        [SerializeField] private InputField _parameterValue;

        private BehaviourOption _behaviourOption;
        
        public void Setup(BehaviourOption behaviourOption, string key, object value)
        {
            _behaviourOption = behaviourOption;
            _parameterName.text = key;
            _parameterValue.text = value.ToString();
        }

        public void GetParameterValues(ref Dictionary<string, object> parameters)
        {
            parameters.Add(_parameterName.text, Convert.ToInt32(_parameterValue.text, CultureInfo.InvariantCulture));
        }

        public void SetParameter()
        {
            _behaviourOption.EntityBehaviourData.SetParameter(_parameterName.text, Convert.ToInt32(_parameterValue.text, CultureInfo.InvariantCulture));
        }
    }
}