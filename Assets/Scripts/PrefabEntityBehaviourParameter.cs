using System;
using System.Collections.Generic;
using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class PrefabEntityBehaviourParameter : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private UnityEngine.UI.Text _parameterName;
        [SerializeField] private UnityEngine.UI.InputField _parameterValue;

        public void Setup(string key, object value)
        {
            _parameterName.text = key;
            _parameterValue.text = value.ToString();
        }

        public void GetParameterValues(ref Dictionary<string, object> parameters)
        {
            parameters.Add(_parameterName.text, Convert.ToInt32(_parameterValue.text));
        }
    }
}