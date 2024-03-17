using UnityEngine;
using UnityEngine.Events;

namespace Greatwanz.GameMaker
{
    [System.Serializable]
    public class IntUnityEvent : UnityEvent<int>
    {
    }

    public class GameEventListenerIntOneParam : GameEventListenerOneParam<int>
    {
        [SerializeField] protected IntGameEvent gameEvent;
        [SerializeField] protected IntUnityEvent response;

        public override void OnEnableLogic()
        {
            if (gameEvent != null)
                gameEvent.Register(this);
        }

        public override void OnDisableLogic()
        {
            if (gameEvent != null)
                gameEvent.UnRegister(this);
        }

        public override void Response(int val)
        {
            response.Invoke(val);
        }
    }
}
