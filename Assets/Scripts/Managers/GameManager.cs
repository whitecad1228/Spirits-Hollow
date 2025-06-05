using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InventoryManager inventoryManager;
    public TilemapManager tilemapManager;
    public ItemManager itemManager;
    public UIManager uiManager;
    public CropManager cropManager;

    public GameObject[] persistentObjects;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // Destroy(this.gameObject);
            CleanUp();
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        MarkPersistentObjects();

        inventoryManager = GetComponent<InventoryManager>();
        tilemapManager = GetComponent<TilemapManager>();
        uiManager = GetComponent<UIManager>();
        itemManager = GetComponent<ItemManager>();
        cropManager = GetComponent<CropManager>();

    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUp()
    {
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);
        }
        Destroy(this.gameObject);
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
