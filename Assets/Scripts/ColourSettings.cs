using UnityEngine;

namespace Greatwanz.GameMaker
{
    [CreateAssetMenu(menuName = "Settings/Colour")]
    public class ColourSettings : ScriptableObject
    {
        [Header("Colour")] 
        [SerializeField] private Color _defaultColour;
        [SerializeField] private Color _selectedColour;

        public Color DefaultColour => _defaultColour;
        public Color SelectedColour => _selectedColour;
    }
}