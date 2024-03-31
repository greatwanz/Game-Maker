using UnityEngine;

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

        private EntityType _editorOption;
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

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
                _editorOption.OnDrop(transform.position, _currentBehaviourDataSet.Items);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _onToggleEditorEvent.Raise(true);
                gameObject.SetActive(false);
            }
            else
            {
                transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(_camera.transform.position.z)));
                gameObject.SetActive(true);
            }
        }

        public void OnDragEditorOption(EditorOptionType option)
        {
            if (option is EntityType e)
            {
                gameObject.SetActive(true);
                _dragEntity.Setup(e);
                _editorOption = e;
            }
        }
    }
}
