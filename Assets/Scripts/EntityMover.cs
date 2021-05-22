using UnityEngine;
using UnityEngine.EventSystems;

namespace Greatwanz.GameMaker
{
    public class EntityMover : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private EditorPanelTypeVariable _editorPanelTypeVariable;
        [SerializeField] private EditorPanelType _prefabEditorPanelType;
        [Header("Game Event")]
        [SerializeField] private EditorOptionGameEvent _dragEditorOptionEvent;
        [SerializeField] private EntityGameEvent _saveEntityEvent;

        private Vector3 _curScreenPoint;
        private Ray _ray;
        private RaycastHit _hit;
        private Entity _movedEntity;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                _ray = Camera.main.ScreenPointToRay(_curScreenPoint);

                if (Physics.Raycast(_ray, out _hit))
                {
                    Entity entity = _hit.transform.GetComponent<Entity>();
                    if (entity)
                    {
                        _movedEntity = entity;
                        _dragEditorOptionEvent.Raise(_movedEntity.entityType);
                        _movedEntity.gameObject.SetActive(false);
                    }
                }
            }
            else if (_movedEntity && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(1)))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    _curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                    _movedEntity.transform.position = Camera.main.ScreenToWorldPoint(_curScreenPoint);
                    _movedEntity.gameObject.SetActive(true);
                }
                else
                {
                    if (_editorPanelTypeVariable.value == _prefabEditorPanelType)
                    {
                        _saveEntityEvent.Raise(_movedEntity);
                    }
                    Destroy(_movedEntity.gameObject);
                }
                _movedEntity = null;
            }
        }
    }
}
