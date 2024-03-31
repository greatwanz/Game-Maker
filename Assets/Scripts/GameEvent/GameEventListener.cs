using UnityEngine;
using UnityEngine.Events;

namespace Greatwanz.GameMaker
{
    public abstract class GameEventListener : MonoBehaviour
    {
        [SerializeField] protected GameEvent gameEvent;
        [SerializeField] protected UnityEvent response;

        void OnEnable()
        {
            OnEnableLogic();
        }

        void OnDisable()
        {
            OnDisableLogic();
        }

        public void OnEnableLogic()
        {
            if (gameEvent != null)
                gameEvent.Register(this);
        }

        public void OnDisableLogic()
        {
            if (gameEvent != null)
                gameEvent.UnRegister(this);
        }

        public void Response()
        {
            response.Invoke();
        }
    }
}