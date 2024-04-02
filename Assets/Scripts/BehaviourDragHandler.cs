using UnityEngine;
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
            transform.position = Input.mousePosition;
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                gameObject.SetActive(false);
                _onToggleEditorEvent.Raise(true);
                _editorOption.OnDrop(transform.position);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _onToggleEditorEvent.Raise(true);
                gameObject.SetActive(false);
                Input.ResetInputAxes();
            }
            else
            {
                transform.position = Input.mousePosition;
            }
        }

        public void OnDragEditorOption(EditorOptionType option)
        {
            if (option is BehaviourOptionType e)
            {
                gameObject.SetActive(true);
                _dragImage.sprite = e.thumbnail;
                _editorOption = e;
            }
        }
    }
}
