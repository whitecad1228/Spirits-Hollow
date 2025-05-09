using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : MonoBehaviour
{
    public Tilemap cropTilemap;
    public Tilemap cropExtraTilemap;
    private List<CropTileInstance> activeCrops = new();

    public void PlantCrop(Vector3Int tilePos, CropData data)
    {
        cropTilemap.SetTile(tilePos, data.growthStages[0]);
        if(data.growthStagesExtra[0] != null){
            cropExtraTilemap.SetTile(tilePos += Vector3Int.up, data.growthStagesExtra[0]);
        }
        activeCrops.Add(new CropTileInstance
        {
            tilePosition = tilePos,
            cropData = data,
            currentStage = 0,
            growthTimer = 0f
        });
    }

    public void NextDay(){

        foreach (var crop in activeCrops)
        {
            crop.growthTimer += 1;

            if (crop.currentStage < crop.cropData.growthStages.Length - 1 && crop.growthTimer >= crop.cropData.stageTimes[crop.currentStage])
            {
                crop.growthTimer = 0f;
                crop.currentStage++;
                cropTilemap.SetTile(crop.tilePosition, crop.cropData.growthStages[crop.currentStage]);
                if(crop.cropData.growthStagesExtra[crop.currentStage] != null){
                    cropExtraTilemap.SetTile(crop.tilePosition + Vector3Int.up, crop.cropData.growthStagesExtra[crop.currentStage]);
                }
            }
        }
    }

    public void HarvestCrop(Vector3Int tilePos)
    {
        var crop = activeCrops.FirstOrDefault(c => c.tilePosition == tilePos);
        if (crop != null && crop.currentStage == crop.cropData.growthStages.Length - 1)
        {
            // Harvest logic here (give item, remove tile, etc.)
            GameManager.instance.itemManager.CreateCollectable(tilePos,crop.cropData.harvestData,Vector3.zero);
            if(crop.cropData.MultiHarvest){
                crop.growthTimer = 0f;
                crop.currentStage--;

                cropTilemap.SetTile(tilePos, crop.cropData.growthStages[crop.currentStage]);
                if(crop.cropData.growthStagesExtra[crop.currentStage] != null){
                    cropExtraTilemap.SetTile(tilePos + Vector3Int.up, crop.cropData.growthStagesExtra[crop.currentStage]);
                }
            }else{
                cropTilemap.SetTile(tilePos, null);
                if(crop.cropData.growthStagesExtra[crop.currentStage] != null){
                    cropTilemap.SetTile(tilePos + Vector3Int.up, null);
                }
                activeCrops.Remove(crop);
            }
            
        }
    }

}

