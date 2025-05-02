using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    

    [Header("Only gameplay")]
    public ItemType type;
    public ActionType actionType;
    public int ID;
    public CropData cropData;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite inventorySprite;
    public Sprite collectableSprite;
    public int maxStackCount = 99;


}

public enum ItemType{
    None,
    Tool,
    Seed,
    Food

}

public enum ActionType {
    None,
    Mine,
    Hoe,
    Water, 
    Chop,
    Fish,
    Light, 
    Cut, 
    Plant

}
