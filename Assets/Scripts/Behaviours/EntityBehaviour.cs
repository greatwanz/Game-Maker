using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public abstract class EntityBehaviour : EditorOptionType
    {
        public abstract void Execute(Entity e);

        public override void OnPointerDown(PointerEventData eventData)
        {
        }

        public override void OnDrop()
        {
        }
    }
}
