using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{


    public List<ItemData> items;

    void Awake() {
        items = Resources.LoadAll<ItemData>("InventoryItems").ToList<ItemData>();

        for(int i = 0; i < items.Count; i++){
            items[i].ID = i;
        }
    }

    public ItemData GetItemData(String id){
        foreach(ItemData item in items){
            if (item.name == id){
                return item;
            }
        }
        return null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
