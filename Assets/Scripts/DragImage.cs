using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class DragImage : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private Entity dragEntityPrefab;
        [Header("References")]
        [SerializeField] private Image dragImage;

        private EditorOptionType editorOption;
        private Entity dragEntity;

        private void Start()
        {
            transform.position = Input.mousePosition;
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!IsPointerOverUIObject())
                {
                    gameObject.SetActive(false);
                    dragEntity.gameObject.SetActive(false);
                    editorOption.OnDrop();
                }
                else
                {
                    gameObject.SetActive(false);
                    dragEntity.gameObject.SetActive(false);
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                gameObject.SetActive(false);
                dragEntity.gameObject.SetActive(false);
            }
            else
            {
                if (IsPointerOverUIObject())
                {
                    dragImage.enabled = true;
                    transform.position = Input.mousePosition;
                    dragEntity.gameObject.SetActive(false);
                }
                else
                {
                    dragImage.enabled = false;
                    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                    dragEntity.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
                    dragEntity.gameObject.SetActive(true);
                }
            }
        }

        public void EnableDragImage(EditorOptionType option)
        {
            if (dragEntity == null)
            {
                dragEntity = Instantiate(dragEntityPrefab);
                dragEntity.gameObject.SetActive(false);
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
                dragEntity.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
                dragEntity.name = "Drag Entity";
            }
            else
            {
                option.Setup(dragEntity);
            }

            gameObject.SetActive(true);
            dragImage.sprite = option.thumbnail;
            editorOption = option;
        }

        public static bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
