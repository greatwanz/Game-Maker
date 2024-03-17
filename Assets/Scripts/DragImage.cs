using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class DragImage : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private Entity _dragEntityPrefab;
        [Header("References")]
        [SerializeField] private Image _dragImage;

        private EditorOptionType _editorOption;
        private Entity _dragEntity;

        private bool _canInstantiate;

        private void OnEnable()
        {
            transform.position = Input.mousePosition;
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                gameObject.SetActive(false);
                _dragEntity.gameObject.SetActive(false);

                if (_canInstantiate && !EventSystem.current.IsPointerOverGameObject())
                {
                    _editorOption.OnDrop(_dragEntity.transform.position);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                gameObject.SetActive(false);
                _dragEntity.gameObject.SetActive(false);
            }
            else
            {
                ShowDrag();
            }
        }

        public void EnableDragImage(EditorOptionType option)
        {
            _canInstantiate = EventSystem.current.IsPointerOverGameObject();

            if (!_dragEntity)
            {
                _dragEntity = Instantiate(_dragEntityPrefab);
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                _dragEntity.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
            }

            option.Setup(_dragEntity);
            gameObject.SetActive(true);
            _dragImage.sprite = option.thumbnail;
            _editorOption = option;
        }

        private void ShowDrag()
        {
            if (_editorOption.HasMesh())
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    _dragEntity.gameObject.SetActive(false);
                    _dragImage.enabled = true;
                    transform.position = Input.mousePosition;
                }
                else
                {
                    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                    _dragEntity.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
                    _dragEntity.gameObject.SetActive(true);
                    _dragImage.enabled = false;
                }
            }
            else
            {
                _dragEntity.gameObject.SetActive(false);
                _dragImage.enabled = true;
                transform.position = Input.mousePosition;
            }
        }
    }
}
