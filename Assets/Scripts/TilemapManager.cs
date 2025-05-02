using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{

    public TileBase cursorTile;

    [Header("Tilemaps")]
    public Tilemap groundTilemap;
    public Tilemap wateredTilemap;
    public Tilemap cropsTilemap;

    public Tilemap cursorTilemap;
  
    public Dictionary<TargetTilemapLayer, Tilemap> layerMap;

    private Vector3Int previousTileCoordinate;

    void Awake() {
        Debug.Log("test");
        layerMap = new Dictionary<TargetTilemapLayer, Tilemap> {
            { TargetTilemapLayer.Ground, groundTilemap },
            { TargetTilemapLayer.Watered, wateredTilemap },
            { TargetTilemapLayer.Crops, cropsTilemap }
        };
        Debug.Assert(groundTilemap != null, "Ground tilemap is not assigned!");
        Debug.Assert(wateredTilemap != null, "Watered tilemap is not assigned!");
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
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCoordinate = cursorTilemap.WorldToCell(mouseWorldPos);

        if(tileCoordinate != previousTileCoordinate && !GameManager.instance.uiManager.Paused){
            cursorTilemap.SetTile(previousTileCoordinate, null);
 	        cursorTilemap.SetTile(tileCoordinate,cursorTile);
            previousTileCoordinate = tileCoordinate; 	
        }
    }

    public void Interact(Vector3 mouseWorldPos){
        Vector3Int position = cursorTilemap.WorldToCell(mouseWorldPos);
        foreach(var tilemap in layerMap.Values){
            Debug.Log(tilemap);
            TileBase tile = tilemap.GetTile(position);
            ItemData selectedItem = GameManager.instance.inventoryManager.GetSelectedItem(false);

            
            if (tile is InteractableTile interactableTile && interactableTile.isInteractable && selectedItem.actionType == interactableTile.actionType) {
                TileBase tileToChangeTo = null;
                if(selectedItem.type == ItemType.Tool){
                    tileToChangeTo = interactableTile.tileToChangeTo;
                }
                if(selectedItem.type == ItemType.Seed){
                    GameManager.instance.cropManager.PlantCrop(position,selectedItem.cropData);
                    return;
                }

                Tilemap targetTilemap = GetTilemap(interactableTile.targetTilemapLayer);
                if(targetTilemap == null){
                    Debug.Log("error with tilemap");
                }
                targetTilemap.SetTile(position, tileToChangeTo);
                return;
            }

        }

    }

}
