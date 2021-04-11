using System;
using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class EntityEditor : MonoBehaviour
    {
        [SerializeField] private Button[] panelSelectButtons;
        [SerializeField] private NonDraggableScrollRect entitiesScrollView;
        [SerializeField] private NonDraggableScrollRect behavioursScrollView;
        [Space]
        [SerializeField] private EditorOption entityOptionPrefab;
        [SerializeField] private EntityData[] entityData;
        [SerializeField] private EntityBehaviour[] behaviourData;

        void Start()
        {
            foreach (var data in entityData)
            {
                EditorOption e = Instantiate(entityOptionPrefab, entitiesScrollView.content);
                e.Setup(data);
            }

            foreach (var data in behaviourData)
            {
                EditorOption e = Instantiate(entityOptionPrefab, behavioursScrollView.content);
                e.Setup(data);
            }
        }
    }
}