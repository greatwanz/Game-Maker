using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EditorPanelType : ScriptableObject
    {
        [Header("Definition")]
        [SerializeField] private bool _isDefaultEnabled = true;
        [SerializeField] private bool _isInputFieldType = false;
        [SerializeField] private string _panelName;
        [SerializeField] private EditorOptionType[] _entityOptionTypes;

        public bool IsDefaultEnabled => _isDefaultEnabled;
        public bool IsInputFieldType => _isInputFieldType;
        public string PanelName => _panelName;
        public EditorOptionType[] EntityOptionTypes => _entityOptionTypes;
    }
}
