using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public abstract class EditorOptionType : ScriptableObject
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
    }
}
