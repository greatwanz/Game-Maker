using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EditorPanelType : ScriptableObject
    {
        [Header("Definition")]
        [SerializeField] private string _panelName;
        [SerializeField] private EditorOptionType[] _entityOptionTypes;

        public string PanelName => _panelName;
        public EditorOptionType[] EntityOptionTypes => _entityOptionTypes;
    }
}
