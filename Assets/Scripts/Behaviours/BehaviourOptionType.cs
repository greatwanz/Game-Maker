using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Greatwanz.GameMaker
{
    public abstract class BehaviourOptionType : EditorOptionType
    {
        public abstract void Execute(Entity e, Dictionary<string, object> paramValues);

        public override Entity OnDrop(Vector3 position)
        {
            var mousePosition = Mouse.current.position.ReadValue();
            Vector3 curScreenPoint = new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
            Ray ray = Camera.main.ScreenPointToRay(curScreenPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Entity entity = hit.transform.GetComponent<Entity>();
                if (entity)
                {
                    entity.AddBehaviour(new EntityBehaviourData(this, GetDefaultParameters()));
                }

                return entity;
            }

            return null;
        }

        protected abstract Dictionary<string, object> GetDefaultParameters();
    }
}
