using UnityEngine;
using UnityEngine.Events;

namespace Greatwanz.GameMaker
{
    public abstract class GameEventListenerOneParam<T> : MonoBehaviour
    {
        [SerializeField] protected GameEventOneParam<T> gameEvent;
        [SerializeField] protected UnityEvent<T> response;

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

        public void Response(T val)
        {
            response.Invoke(val);
        }
    }
}
