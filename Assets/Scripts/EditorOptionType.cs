using UnityEngine;

namespace Greatwanz.GameMaker
{
    public abstract class EditorOptionType : ScriptableObject
    {
        [Header("Definition")]
        [SerializeField] private string _optionName;
        [SerializeField] private Sprite _thumbnail;

        public string optionName => _optionName;

        public Sprite thumbnail => _thumbnail;

        public abstract Entity OnDrop(Vector3 position);
    }
}
