using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Interactable Tile")]
public class InteractableTile : RuleTile<InteractableTile.Neighbor>
{
    public bool isInteractable;
    public TileBase tileToChangeTo;

    public Tilemap tilemap;

    public TargetTilemapLayer targetTilemapLayer;

    public class Neighbor : RuleTile.TilingRule.Neighbor { }
}
