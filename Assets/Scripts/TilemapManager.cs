using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{

    [SerializeField] private Tilemap InteractableTilemap;

    // [SerializeField] private Tile[] InteractableTiles;
    // [SerializeField] private RuleTile[] InteractableRuleTiles;
    // [SerializeField] private RuleTile[] InteractableRuleTileResults;

    public Tilemap groundTilemap;
    public Tilemap wateredTilemap;
    public Tilemap cropsTilemap;
    public Tilemap decorationsTilemap;

    [SerializeField]    
    public Dictionary<TargetTilemapLayer, Tilemap> layerMap;

    void Awake() {
        layerMap = new Dictionary<TargetTilemapLayer, Tilemap> {
            { TargetTilemapLayer.Ground, groundTilemap },
            { TargetTilemapLayer.Watered, wateredTilemap }
        };
    }

    public Tilemap GetTilemap(TargetTilemapLayer layer) {
        return layerMap.TryGetValue(layer, out var tilemap) ? tilemap : null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Vector3Int position){
        TileBase tile = InteractableTilemap.GetTile(position);

        if (tile is InteractableTile interactableTile && interactableTile.isInteractable) {
            Tilemap tilemap = GetTilemap(interactableTile.targetTilemapLayer);
            tilemap.SetTile(position, interactableTile.tileToChangeTo);
        }

    }

}
