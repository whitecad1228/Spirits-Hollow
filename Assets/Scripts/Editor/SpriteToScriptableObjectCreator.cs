using UnityEngine;
using UnityEditor;
using System.IO;

public class SpriteToScriptableObjectCreator
{
    // [MenuItem("Tools/Create ScriptableObjects From Sprites")]
    // public static void CreateScriptableObjects()
    // {
    //     Debug.Log("Script started!");

    //     object selectedObjectsTest = Selection.objects;
    //     Debug.Log(selectedObjectsTest);
    //     Debug.Log(Selection.gameObjects);
    //     Debug.Log(Selection.count);

    //     Object[] selectedObjects = Selection.GetFiltered(typeof(Sprite), SelectionMode.DeepAssets);
    //     Debug.Log("Sprites found: " + selectedObjects.Length);

    //     foreach (Object obj in selectedObjects)
    //     {
    //         Sprite sprite = obj as Sprite;
    //         if (sprite != null)
    //         {
    //             Item asset = ScriptableObject.CreateInstance<Item>();
    //             asset.sprite = sprite;

    //             string path = "Assets/Resources";
    //             if (!Directory.Exists(path))
    //                 Directory.CreateDirectory(path);

    //             string assetPath = $"{path}/{sprite.name}.asset";
    //             AssetDatabase.CreateAsset(asset, assetPath);
    //         }
    //     }

    //     AssetDatabase.SaveAssets();
    //     AssetDatabase.Refresh();
    //     Debug.Log("ScriptableObjects created!");
    // }

    [MenuItem("Tools/Create ScriptableObjects From Selected Sprites or Textures")]
    public static void CreateScriptableObjects()
    {
        Object[] selectedObjects = Selection.GetFiltered<Object>(SelectionMode.Assets);
        int createdCount = 0;

        foreach (Object obj in selectedObjects)
        {
            if (obj is Texture2D texture)
            {
                string path = AssetDatabase.GetAssetPath(texture);
                Object[] subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);

                foreach (Object subAsset in subAssets)
                {
                    if (subAsset is Sprite sprite)
                    {
                        CreateSpriteData(sprite);
                        createdCount++;
                    }
                }
            }
            else if (obj is Sprite sprite)
            {
                CreateSpriteData(sprite);
                createdCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Created {createdCount} ScriptableObject(s).");
    }

    private static void CreateSpriteData(Sprite sprite)
    {
        ItemData asset = ScriptableObject.CreateInstance<ItemData>();
        asset.inventorySprite = sprite;

        string path = "Assets/Resources";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string assetPath = $"{path}/{sprite.name}.asset";
        AssetDatabase.CreateAsset(asset, assetPath);
    }

}
