using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class BehaviourOptionParameter : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private Text _parameterName;
        [SerializeField] private InputField _parameterValue;
        [SerializeField] private Toggle _parameterToggle;

        private BehaviourOption _behaviourOption;

        private object _startValue;
        
        public void Setup(BehaviourOption behaviourOption, string key, object value)
        {
            _behaviourOption = behaviourOption;
            _parameterName.text = key;
            _startValue = value;
            if(!(value is bool)) _parameterValue.text = value.ToString();
            _parameterValue.gameObject.SetActive(!(value is bool));
            _parameterToggle.gameObject.SetActive(value is bool);

            if(value is int)
            {
                _parameterValue.contentType = InputField.ContentType.IntegerNumber;
            }
            else if (value is float || value is double)
            {
                _parameterValue.contentType = InputField.ContentType.DecimalNumber;
            }
            else if (value is string)
            {
                _parameterValue.contentType = InputField.ContentType.Standard;
            }
            else if (value is bool)
            {
                _parameterToggle.SetIsOnWithoutNotify((bool)_startValue);
            }
        }

        public void GetParameterValues(ref Dictionary<string, object> parameters)
        {
            if (_startValue is int)
            {
                parameters.Add(_parameterName.text, Convert.ToInt32(_parameterValue.text, CultureInfo.InvariantCulture));
            }
            else if (_startValue is float)
            {
                parameters.Add(_parameterName.text, Convert.ToSingle(_parameterValue.text, CultureInfo.InvariantCulture));
            }
            else if (_startValue is double)
            {
                parameters.Add(_parameterName.text, Convert.ToDouble(_parameterValue.text, CultureInfo.InvariantCulture));
            }
            else if (_startValue is string)
            {
                parameters.Add(_parameterName.text, _parameterValue.text);
            }
            else if (_startValue is bool)
            {
                parameters.Add(_parameterName.text, _parameterToggle.isOn);
            }
        }

        public void SetParameter()
        {
            if(_startValue is int)
            {
                _behaviourOption.EntityBehaviourData.SetParameter(_parameterName.text, Convert.ToInt32(_parameterValue.text, CultureInfo.InvariantCulture));
            }
            else if (_startValue is float)
            {
                _behaviourOption.EntityBehaviourData.SetParameter(_parameterName.text, Convert.ToSingle(_parameterValue.text, CultureInfo.InvariantCulture));
            }
            else if (_startValue is double)
            {
                _behaviourOption.EntityBehaviourData.SetParameter(_parameterName.text, Convert.ToDouble(_parameterValue.text, CultureInfo.InvariantCulture));
            }
            else if (_startValue is string)
            {
                _behaviourOption.EntityBehaviourData.SetParameter(_parameterName.text, _parameterValue.text);
            }
            else if (_startValue is bool)
            {
                _behaviourOption.EntityBehaviourData.SetParameter(_parameterName.text, _parameterToggle.isOn);
            }
        }
    }
}