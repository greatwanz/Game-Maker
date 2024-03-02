using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EditorPanelButton : MonoBehaviour
    {
        [Header("Definition")]
        [SerializeField] private Text _buttonText;
        [SerializeField] private Button _button;

        public void Setup(string text)
        {
            _buttonText.text = text;
        }

        public void Bind(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }
    }
}
