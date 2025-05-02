using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/CropData")]
public class CropData : ScriptableObject
{
    public string cropName;
    public TileBase[] growthStages; // Each index = stage sprite
    public TileBase[] growthStagesExtra; // Each index = stage sprite
    public int[] stageTimes; // Time to reach each stage
}
