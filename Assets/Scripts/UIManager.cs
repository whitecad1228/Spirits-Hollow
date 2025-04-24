using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    [SerializeField] private KeyCode InventoryButton;
    [SerializeField] private GameObject InventoryUI;
    private bool InventoryOpened = false;
    private float timeScale = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(InventoryButton)){
            if(InventoryOpened){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        InventoryUI.SetActive(false);
        Time.timeScale = timeScale;
        InventoryOpened = false;
    }

    public void Pause(){
        InventoryUI.SetActive(true);
        Time.timeScale = 0f;
        InventoryOpened = true;
    }
}
