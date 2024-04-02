using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Greatwanz.GameMaker
{
    public class Utility
    {
        public static bool IsPointerOverUIElement()
        {
            var layer = LayerMask.NameToLayer("UI");
            return IsPointerOverUIElement(GetEventSystemRaycastResults(), layer);
        }
        
        public static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Mouse.current.position.ReadValue();
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults;
        }

        private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults, int layer)
        {
            for (int index = 0; index < eventSystemRaycastResults.Count; index++)
            {
                RaycastResult curRaycastResult = eventSystemRaycastResults[index];
                if (curRaycastResult.gameObject.layer == layer)
                    return true;
            }

            return false;
        }
        
    }
}