using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public class PointerDownHandler : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent<PointerEventData> OnPointerDownEvent;
        public void OnPointerDown(PointerEventData data)
        {
            if (Input.GetMouseButtonDown(0) && OnPointerDownEvent != null)
            {
                OnPointerDownEvent.Invoke(data);
            }
        }
    }
}
