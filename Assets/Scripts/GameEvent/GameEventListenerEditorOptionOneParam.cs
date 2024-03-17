using UnityEngine;
using UnityEngine.Events;

namespace Greatwanz.GameMaker
{
    [System.Serializable]
    public class EditorOptionUnityEvent : UnityEvent<EditorOptionType>
    {
    }

    public class GameEventListenerEditorOptionOneParam : GameEventListenerOneParam<EditorOptionType>
    {
        [SerializeField] protected EditorOptionGameEvent gameEvent;
        [SerializeField] protected EditorOptionUnityEvent response;

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

        public override void Response(EditorOptionType val)
        {
            response.Invoke(val);
        }
    }
}
