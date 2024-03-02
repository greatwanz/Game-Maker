using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public abstract class EditorOptionType : ScriptableObject, IEquatable<EditorOptionType>
    {
        [Header("Definition")]
        [SerializeField] private string _optionName;
        [SerializeField] private Sprite _thumbnail;

        public string optionName => _optionName;

        public Sprite thumbnail => _thumbnail;

        public abstract void Setup(Entity e);

        public abstract void OnPointerDown(PointerEventData eventData);

        public abstract void OnDrop(Vector3 position);

        public abstract bool HasMesh();

        public override bool Equals(object obj) => this.Equals(obj as EditorOptionType);

        public bool Equals(EditorOptionType other)
        {
            return GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }

        public static bool operator ==(EditorOptionType lhs, EditorOptionType rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(EditorOptionType lhs, EditorOptionType rhs) => !(lhs == rhs);
    }
}
