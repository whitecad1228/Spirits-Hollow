using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float speed = 10;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private KeyCode InteractButton;

    [SerializeField] private KeyCode ThrowButton;

    [SerializeField] private Item ItemPrefab;

    private Rigidbody2D rb;
    private const string _Horizontal = "Horizontal";
    private const string _Vertical = "Vertical";
    private const string _LastHorizontal = "LastHorizontal";
    private const string _LastVertical = "LastVertical";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(InteractButton)){
            // Vector3Int position = new Vector3Int((int)(transform.position.x - 1f),(int)(transform.position.y - 1f),0);
            // Debug.Log(position);
            
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameManager.instance.tilemapManager.Interact(mouseWorldPos);
        }

        if(Input.GetKeyDown(ThrowButton)){
            ThrowItem();
        }


    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal,vertical);
        rb.linearVelocity = direction.normalized * speed;

        animator.SetFloat(_Horizontal,horizontal);
        animator.SetFloat(_Vertical,vertical);


        

        if(direction != Vector2.zero){
            spriteRenderer.flipX = horizontal < 0f;
            animator.SetFloat(_LastHorizontal,horizontal);
            animator.SetFloat(_LastVertical,vertical);
        }
    }

    void ThrowItem(){
        ItemData item = GameManager.instance.inventoryManager.GetSelectedItem(true);
        if(item != null){
            Vector2 spawnLocation = transform.position;

            Vector2 spawnOffset = Random.insideUnitCircle * 1.5f;

            Item droppedItem = Instantiate(ItemPrefab, spawnLocation + spawnOffset, Quaternion.identity);
            droppedItem.data = item;
            droppedItem.rb.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
        }
        
    }

    public void CollectItem(Item item){
        GameManager.instance.inventoryManager.AddItem(item.data);
    }
}
