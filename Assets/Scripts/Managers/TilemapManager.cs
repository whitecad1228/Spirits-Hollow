using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{

    public TileBase cursorTile;

    [Header("Tilemaps")]

    public String groundTilemapName;
    public String wateredTilemapName;
    public String cropsTilemapName;
    public String cursorTilemapName;

    // public Tilemap groundTilemap;
    // public Tilemap wateredTilemap;
    // public Tilemap cropsTilemap;
    public Tilemap cursorTilemap;

    public Dictionary<TargetTilemapLayer, Tilemap> layerMap;

    private Vector3Int previousTileCoordinate;

    void Awake()
    {
        // AssignTilemaps();
    }

    public Tilemap GetTilemap(TargetTilemapLayer layer)
    {
        return layerMap.TryGetValue(layer, out var tilemap) ? tilemap : null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignTilemaps();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCoordinate = cursorTilemap.WorldToCell(mouseWorldPos);

        if (tileCoordinate != previousTileCoordinate && !GameManager.instance.uiManager.Paused)
        {
            cursorTilemap.SetTile(previousTileCoordinate, null);
            cursorTilemap.SetTile(tileCoordinate, cursorTile);
            previousTileCoordinate = tileCoordinate;
        }
    }

    public ActionType Interact(Vector3 mouseWorldPos)
    {
        Vector3Int position = cursorTilemap.WorldToCell(mouseWorldPos);
        foreach (var tilemap in layerMap.Values)
        {
            Debug.Log(tilemap);
            TileBase tile = tilemap.GetTile(position);
            ItemData selectedItem = GameManager.instance.inventoryManager.GetSelectedItem(false);
            if (selectedItem != null)
            {
                if (tile is InteractableTile interactableTile && interactableTile.isInteractable && selectedItem.actionType == interactableTile.actionType)
                {
                    TileBase tileToChangeTo = null;
                    if (selectedItem.type == ItemType.Tool)
                    {
                        tileToChangeTo = interactableTile.tileToChangeTo;
                    }
                    if (selectedItem.type == ItemType.Seed)
                    {
                        GameManager.instance.cropManager.PlantCrop(position, selectedItem.cropData);
                        return selectedItem.actionType;
                    }

                    Tilemap targetTilemap = GetTilemap(interactableTile.targetTilemapLayer);
                    if (targetTilemap == null)
                    {
                        Debug.Log("error with tilemap");
                    }
                    targetTilemap.SetTile(position, tileToChangeTo);
                    return selectedItem.actionType;
                }
            }
            GameManager.instance.cropManager.HarvestCrop(position);
        }
        return ActionType.None;

    }

    public void waterSpell(Vector3 playerPosition)
    {

        Vector3Int centerPosition = cursorTilemap.WorldToCell(playerPosition);
        List<Vector3Int> positions = new List<Vector3Int>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                positions.Add(new Vector3Int((int)centerPosition.x + x, (int)centerPosition.y + y, 0));
                Debug.Log($"{(int)centerPosition.x + x}, {(int)centerPosition.y + y}");
            }
        }

        foreach (var position in positions)
        {
            foreach (var tilemap in layerMap.Values)
            {
                TileBase tile = tilemap.GetTile(position);
                ItemData selectedItem = GameManager.instance.inventoryManager.GetSelectedItem(false);
                if (tile is InteractableTile interactableTile && interactableTile.isInteractable && interactableTile.actionType == ActionType.Water)
                {
                    TileBase tileToChangeTo = interactableTile.tileToChangeTo;
                    Tilemap targetTilemap = GetTilemap(interactableTile.targetTilemapLayer);
                    if (targetTilemap == null)
                    {
                        Debug.Log("error with tilemap");
                    }
                    targetTilemap.SetTile(position, tileToChangeTo);
                }
            }
        }
        return;
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignTilemaps();
    }

    private void AssignTilemaps()
    {
        Debug.Log("hello");
        layerMap = new Dictionary<TargetTilemapLayer, Tilemap> {
            { TargetTilemapLayer.Ground, GameObject.Find(groundTilemapName)?.GetComponent<Tilemap>() },
            { TargetTilemapLayer.Watered, GameObject.Find(wateredTilemapName)?.GetComponent<Tilemap>() },
            { TargetTilemapLayer.Crops, GameObject.Find(cropsTilemapName)?.GetComponent<Tilemap>() }
        };
        foreach (var tilemap in layerMap.Values)
        {
            Debug.Assert(tilemap != null, "A tilemap is not assigned!");
        }
        cursorTilemap = GameObject.Find(cursorTilemapName)?.GetComponent<Tilemap>();
    }
    
}
