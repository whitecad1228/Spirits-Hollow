using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Interactable Tile")]
public class InteractableTile : RuleTile<InteractableTile.Neighbor>
{
    public bool isInteractable;
    public TileBase tileToChangeTo;

    public TargetTilemapLayer targetTilemapLayer;

    public ActionType actionType;

    public bool interacted = false;

    public class Neighbor : RuleTile.TilingRule.Neighbor { }
}

public enum TargetTilemapLayer {
    Ground,
    Watered,
    Crops
}