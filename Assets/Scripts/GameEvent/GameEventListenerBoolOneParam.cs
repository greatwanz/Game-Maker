using UnityEngine;
using UnityEngine.Events;

namespace Greatwanz.GameMaker
{
    [System.Serializable]
    public class BoolUnityEvent : UnityEvent<bool>
    {
    }

    [System.Serializable]
    public class GameEventListenerBoolOneParam : GameEventListenerOneParam<bool>
    {
        [SerializeField] protected BoolGameEvent gameEvent;
        [SerializeField] protected BoolUnityEvent response;

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

        public override void Response(bool val)
        {
            response.Invoke(val);
        }
    }
}
