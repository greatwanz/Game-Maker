using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EditorPanelType : ScriptableObject
    {
        public string _panelName;
        public EditorOptionType[] _entityOptionTypes;
    }
}
