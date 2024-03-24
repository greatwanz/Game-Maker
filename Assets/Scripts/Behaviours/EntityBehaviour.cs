using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public abstract class EntityBehaviour : EditorOptionType
    {
        public abstract void Execute(Entity e, Dictionary<string, object> paramValues);

        public override void OnPointerDown(PointerEventData eventData)
        {
        }

        public override bool HasMesh()
        {
            return false;
        }

        public override void OnDrop(Vector3 position)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
            Ray ray = Camera.main.ScreenPointToRay(curScreenPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Entity entity = hit.transform.GetComponent<Entity>();
                if (entity)
                {
                    entity.AddBehaviour(new EntityBehaviourData(this, GetDefaultParameters()));
                }
            }
        }

        protected abstract Dictionary<string, object> GetDefaultParameters();
    }
}
