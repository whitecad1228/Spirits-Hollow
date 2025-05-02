using System.Data.Common;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    public ItemData data;
    public SpriteRenderer spriteRenderer;

    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        spriteRenderer.sprite = data.collectableSprite;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if(player){
            
            Item item = GetComponent<Item>();
            if(item != null){
                player.CollectItem(item);
                Destroy(this.gameObject);
            }
        }
    }
}
