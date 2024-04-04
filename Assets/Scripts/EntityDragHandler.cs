using UnityEngine;
using UnityEngine.InputSystem;

namespace Greatwanz.GameMaker
{
    public class EntityDragHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Entity _dragEntity;
        [Header("Data")]
        [SerializeField] private EntityBehaviourDataSet _currentBehaviourDataSet;
        [Header("Game Event")]
        [SerializeField] private BoolGameEvent _onToggleEditorEvent;

        private EntityOptionType _editorOption;
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

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
                _editorOption.OnDrop(transform.position, _currentBehaviourDataSet.Items);
            }
            else if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                _onToggleEditorEvent.Raise(true);
                gameObject.SetActive(false);
            }
            else
            {
                var mousePosition = Mouse.current.position.ReadValue();
                transform.position = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(_camera.transform.position.z)));
                gameObject.SetActive(true);
            }
        }

        public void OnDragEditorOption(EditorOptionType option)
        {
            if (option is EntityOptionType e)
            {
                gameObject.SetActive(true);
                _dragEntity.Setup(e, option.optionName);
                _editorOption = e;
            }
        }
    }
}
