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

        private EditorPanelType _panelType;
        public EditorPanelType PanelType => _panelType;

        public void Setup(string text, EditorPanelType panelType)
        {
            _buttonText.text = text;
            _panelType = panelType;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Bind(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }
    }
}
