using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EditorPanelButton : MonoBehaviour
    {
        [SerializeField] private Text buttonText;
        [SerializeField] private Button button;

        public void Setup(string text)
        {
            buttonText.text = text;
        }

        public void Bind(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }
}
