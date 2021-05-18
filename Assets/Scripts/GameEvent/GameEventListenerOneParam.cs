using UnityEngine;

namespace Greatwanz.GameMaker
{
    public abstract class GameEventListenerOneParam<T> : MonoBehaviour
    {
        /// <summary>
        /// Override this to override the OnEnableLogic()
        /// </summary>
        public abstract void OnEnableLogic();

        void OnEnable()
        {
            OnEnableLogic();
        }

        /// <summary>
        /// Override this to override the OnDisableLogic()
        /// </summary>
        public abstract void OnDisableLogic();

        void OnDisable()
        {
            OnDisableLogic();
        }

        public abstract void Response(T val);
    }
}
