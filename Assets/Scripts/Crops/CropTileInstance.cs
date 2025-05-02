using UnityEngine;

public class CropTileInstance
{
    public Vector3Int tilePosition;
    public CropData cropData;
    public int currentStage = 0;
    public float growthTimer = 0f;
}
