using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public InventorySlot[] inventorySlots;
    public GameObject InventoryObjectPrefab;
    public GameObject SelectorPrefab;

    public GameObject InventoryGroup;

    private GameObject Selector;
    public Item[] items;

    public int selectedSlot = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Selector = Instantiate(SelectorPrefab,inventorySlots[selectedSlot].transform.position, quaternion.identity, parent:InventoryGroup.transform);
        // Selector.transform.position = inventorySlots[selectedSlot].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.inputString != null){
            bool isNumber = int.TryParse(Input.inputString,out int number);
            if(isNumber && number > 0 && number < 6){
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    void ChangeSelectedSlot(int newValue){

        // Selector.transform.SetParent(inventorySlots[newValue].transform, false);
        // Selector.transform.position = inventorySlots[newValue].transform.position;
        // selectedSlot = newValue;
        if(selectedSlot >= 0){
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;

    }

    public void PickupItem(int id){
        AddItem(items[id]);
    }

    public bool AddItem(Item item){

        for(int i = 0; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null && (itemInSlot.item == item) && (itemInSlot.count < item.maxStackCount) && (item.stackable == true)){
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for(int i = 0; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                SpawnNewItem(item,slot);
                return true;
            }
        }
        return false;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryObjectPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use){
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null){
            Item item = itemInSlot.item;
            if(use == true){
                itemInSlot.count--;
                if(itemInSlot.count <= 0){
                    Destroy(itemInSlot.gameObject);
                }else{
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }
}
