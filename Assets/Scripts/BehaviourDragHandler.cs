using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class BehaviourDragHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _dragImage;
        [Header("Game Event")]
        [SerializeField] private BoolGameEvent _onToggleEditorEvent;

        private EditorOptionType _editorOption;

        private void OnEnable()
        {
            transform.position = Mouse.current.position.ReadValue();
        }

        void Update()
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                gameObject.SetActive(false);
                _onToggleEditorEvent.Raise(true);
                _editorOption.OnDrop(transform.position);
            }
            else if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                _onToggleEditorEvent.Raise(true);
                gameObject.SetActive(false);
            }
            else
            {
                transform.position = Mouse.current.position.ReadValue();
            }
        }

        public void OnDragEditorOption(EditorOptionType option)
        {
            if (option is BehaviourOptionType e)
            {
                gameObject.SetActive(true);
                _dragImage.sprite = e.Thumbnail;
                _editorOption = e;
            }
        }
    }
}
