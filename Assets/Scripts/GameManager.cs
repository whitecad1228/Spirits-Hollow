using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InventoryManager inventoryManager;
    public TilemapManager tilemapManager;
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
