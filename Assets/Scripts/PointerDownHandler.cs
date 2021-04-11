using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    [System.Serializable]
    public class PointerEvent : UnityEngine.Events.UnityEvent<PointerEventData> { };

    public class PointerDownHandler : MonoBehaviour, IPointerDownHandler
    {
        public PointerEvent OnPointerDownEvent;
        public void OnPointerDown(PointerEventData data)
        {
            if (OnPointerDownEvent != null)
                OnPointerDownEvent.Invoke(data);
        }
    }
}