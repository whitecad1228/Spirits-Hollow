using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InventoryManager inventoryManager;
    public TilemapManager tilemapManager;
    public ItemManager itemManager;
    public UIManager uiManager;
    public CropManager cropManager;
    
    private void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }else{
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        inventoryManager = GetComponent<InventoryManager>();
        tilemapManager = GetComponent<TilemapManager>();
        uiManager = GetComponent<UIManager>();
        itemManager = GetComponent<ItemManager>();
        cropManager = GetComponent<CropManager>();

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
