using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    

    [Header("Only gameplay")]
    public TileBase tile;
    public ToolboxItemFilterType type;
    public ActionType actionType;
    public Vector2 range = new Vector2Int(5,4);

    [Header("Only UI")]
    public bool stackable = false;

    [Header("Both")]
    public Sprite sprite;

    public int maxStackCount = 64;

}

public enum ItemType{
    None,
    Tool
}

public enum ActionType {
    None,
    Dig,
    Mine
}
