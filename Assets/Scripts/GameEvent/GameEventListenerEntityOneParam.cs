using UnityEngine;
using UnityEngine.Events;

namespace Greatwanz.GameMaker
{
    [System.Serializable]
    public class EntityUnityEvent : UnityEvent<Entity>
    {
    }

    public class GameEventListenerEntityOneParam : GameEventListenerOneParam<Entity>
    {
        [SerializeField] protected EntityGameEvent gameEvent;
        [SerializeField] protected EntityUnityEvent response;

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

        public override void Response(Entity val)
        {
            response.Invoke(val);
        }
    }
}
