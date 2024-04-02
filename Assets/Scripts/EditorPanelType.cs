using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EditorPanelType : ScriptableObject
    {
        [Header("Definition")]
        [SerializeField] private bool _isDefaultEnabled = true;
        [SerializeField] private string _panelName;
        [SerializeField] private EditorOptionType[] _entityOptionTypes;

        public bool IsDefaultEnabled => _isDefaultEnabled;
        public string PanelName => _panelName;
        public EditorOptionType[] EntityOptionTypes => _entityOptionTypes;
    }
}
