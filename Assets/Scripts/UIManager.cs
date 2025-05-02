using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    [SerializeField] private KeyCode InventoryButton;
    [SerializeField] private KeyCode TextButton;
    [SerializeField] private KeyCode CommandButton;
    [SerializeField] private GameObject InventoryUI;

    [SerializeField] private GameObject TextUI;

    [SerializeField] private TMP_InputField TextUItest;
    private bool InventoryOpened = false;
    private bool TextOpened = false;
    public bool Paused = false;
    private float timeScale = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!TextOpened){
            if(Input.GetKeyDown(InventoryButton)){
                if(InventoryOpened){
                    Resume(InventoryUI);
                    InventoryOpened = false;
                }else{
                    Pause(InventoryUI);
                    InventoryOpened = true;
                }
            }
        }

        if(Input.GetKeyDown(TextButton)){
            if(TextOpened){
                Resume(TextUI);
                TextOpened = false;
            }else{
                Pause(TextUI);
                TextOpened = true;
                TextUItest.text = "";
                Debug.Log(TextUItest.isFocused);
                TextUItest.Select();
            }
        }
        if(Input.GetKeyDown(CommandButton)){
            if(!TextOpened){
                Pause(TextUI);
                TextOpened = true;
                TextUItest.text = "/";
                Debug.Log(TextUItest.isFocused);
                TextUItest.Select();
            }
        }
    }

    public void Resume(GameObject ui){
        ui.SetActive(false);
        Time.timeScale = timeScale;
        Paused = false;
    }

    public void Pause(GameObject ui){
        ui.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void Readtext(String text){
        Debug.Log("Readtext:" + text);
        if(text[0] == '/'){

            string[] split = text.Split(' ');
            if(split[0] == "/addItem"){
                if(split[1] != null){
                    GameManager.instance.inventoryManager.PickupItem(split[1]);
                }
                
            }
            if(split[0] == "/getItemID"){
                if(split[1] != null){
                    int ID = GameManager.instance.itemManager.GetItemData(split[1]).ID;
                    Debug.Log($"{split[1]} ID: {ID}");
                }
                
            }
        }
    }

    void debug(){
        Debug.Log(Paused);
        Debug.Log(TextOpened);
        Debug.Log(InventoryOpened);
    }
}
