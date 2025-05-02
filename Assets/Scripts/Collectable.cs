using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
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
