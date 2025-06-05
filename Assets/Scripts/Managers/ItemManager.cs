using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{


    public List<ItemData> items;

    [SerializeField] private Item ItemPrefab;

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

    public void CreateCollectable(Vector2 position,ItemData data, Vector2 direction){
        if(direction == Vector2.zero){
            int horizontal = UnityEngine.Random.Range(-1, 2);
            int vertical = UnityEngine.Random.Range(-1, 2);
            direction = new Vector2(horizontal,vertical);
        }
        Item droppedItem = Instantiate(ItemPrefab, position + direction, Quaternion.identity);
        droppedItem.data = data;
        droppedItem.rb.AddForce(direction * .2f, ForceMode2D.Impulse);
    }
    public void CreateCollectable(Vector3 position,ItemData data, Vector3 direction){
        if(direction == Vector3.zero){
            int horizontal = UnityEngine.Random.Range(-1, 2);
            int vertical = UnityEngine.Random.Range(-1, 2);
            direction = new Vector3(horizontal,vertical,0);
        }
        Item droppedItem = Instantiate(ItemPrefab, position + direction, Quaternion.identity);
        droppedItem.data = data;
        droppedItem.rb.AddForce(direction * .2f, ForceMode2D.Impulse);
    }
}
