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

        private EditorOptionType editorOption;
        private Entity dragEntity;
        private List<RaycastResult> results = new List<RaycastResult>();

        private void OnEnable()
        {
            transform.position = Input.mousePosition;
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                gameObject.SetActive(false);
                dragEntity.gameObject.SetActive(false);

                if (!IsPointerOverUIObject())
                {
                    editorOption.OnDrop();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                gameObject.SetActive(false);
                dragEntity.gameObject.SetActive(false);
            }
            else
            {
                ShowDrag(editorOption.mesh);
            }
        }

        public void EnableDragImage(EditorOptionType option)
        {
            if (!dragEntity)
            {
                dragEntity = Instantiate(_dragEntityPrefab);
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                dragEntity.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
            }

            option.Setup(dragEntity);
            gameObject.SetActive(true);
            _dragImage.sprite = option.thumbnail;
            editorOption = option;
        }

        private void ShowDrag(bool hasMesh)
        {
            if (hasMesh)
            {
                if (IsPointerOverUIObject())
                {
                    dragEntity.gameObject.SetActive(false);
                    _dragImage.enabled = true;
                    transform.position = Input.mousePosition;
                }
                else
                {
                    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                    dragEntity.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
                    dragEntity.gameObject.SetActive(true);
                    _dragImage.enabled = false;
                }
            }
            else
            {
                dragEntity.gameObject.SetActive(false);
                _dragImage.enabled = true;
                transform.position = Input.mousePosition;
            }
        }

        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
