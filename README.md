# Game maker challenge
https://github.com/lumpn/gamedev-workshop/tree/master/GameMaker

## Editor Mode
- Left click and drag entity type/prefab to instantiate
    - Can be aborted by right clicking while dragging
- Left click and drag behaviour type on top of instantiated entity to add behaviour
    - Can be aborted by right clicking while dragging
- Left click instantiated entity to select
    - Selecting an entity opens the inspector
	- Entity can be renamed using the title bar of the inspector panel
	- All behaviours attached to the entity are displayed
    - Individual behaviours can be deleted by right clicking the behaviour
	- Parameters of behaviours can be edited, and are automatically saved
- Drag selected instantiated entity to reposition
    - Can be aborted by right clicking while dragging, entity returns to previous position (However there is a bug with pointer handlers that sometimes prevents the entity from receiving pointer handler events after using right click abort. Can be worked around entering and exitinig play mode)
- Right click to delete instantiated entity
- Right or left clicking empty space deselects the current selected entity
- Scroll click to save as prefab
	- All behaviours attached to the prefab are displayed
	- Prefabs can be deleted using right click
    - Individual behaviours can be deleted by right clicking the behaviour
	- Parameters of behaviours can be edited, and are automatically saved
- Editor can be hidden using the panel toggle

## Play Mode
- Left click to execute behaviours
- State of entities are restored when returning to editor mode