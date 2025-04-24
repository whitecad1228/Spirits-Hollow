using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour , IBeginDragHandler , IDragHandler, IEndDragHandler
{
    
    public Image Image;
    public TMP_Text countText;
    [HideInInspector] public Item item;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // InitializeItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeItem(Item newItem){
        item = newItem;
        Image.sprite = newItem.sprite;
        RefreshCount();
        
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
}
