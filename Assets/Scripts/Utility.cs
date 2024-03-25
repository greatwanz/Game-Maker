using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public class Utility
    {
        //Returns 'true' if we touched or hovering on Unity UI element.
        public static bool IsPointerOverUIElement()
        {
            var layer = LayerMask.NameToLayer("UI");
            return IsPointerOverUIElement(GetEventSystemRaycastResults(), layer);
        }


        //Returns 'true' if we touched or hovering on Unity UI element.
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


        //Gets all event system raycast results of current mouse or touch position.
        private static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults;
        }
    }
}