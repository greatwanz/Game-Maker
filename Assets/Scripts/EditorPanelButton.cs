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
        [SerializeField] private InputField _inputField;
        [Header("Data")]
        [SerializeField] private EntityVariable _currentEntityVariable;

        private EditorPanelType _panelType;
        public EditorPanelType PanelType => _panelType;

        private void OnDisable()
        {
            if (_panelType.IsInputFieldType && _currentEntityVariable.Value)
            {
                _currentEntityVariable.Value.EntityName = _inputField.text;
            }
        }

        public void Setup(EditorPanelType panelType)
        {
            _panelType = panelType;
            _inputField.gameObject.SetActive(panelType.IsInputFieldType);
            _button.gameObject.SetActive(!panelType.IsInputFieldType);

            if (_button.IsActive())
            {
                _buttonText.text = panelType.PanelName;
            }
        }

        private void OnDestroy()
        {
            if(_button.IsActive()) _button.onClick.RemoveAllListeners();
        }

        public void Bind(UnityAction action)
        {
            if(_button.IsActive()) _button.onClick.AddListener(action);
        }

        public void SetPanelButtonName(string newName)
        {
            if (_panelType.IsInputFieldType)
            {
                _inputField.text = newName;
            }
            else
            {
                _buttonText.text = newName;
            }
            
            _currentEntityVariable.Value.EntityName = newName;
        }
    }
}
