using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu]
    public class EditorPanelType : ScriptableObject
    {
        public string panelName;
        public EditorOptionType[] entityOptionTypes;
    }
}
