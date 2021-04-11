using UnityEngine;
using UnityEngine.EventSystems;

public interface IEditorOption
{
    string optionName { get; }
    Sprite thumbnail { get; }

    void OnPointerDown(PointerEventData eventData);

    void OnEnterOptionArea();
    void OnLeaveOptionArea();

    void OnDrop();
}
