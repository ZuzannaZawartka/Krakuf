using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{


    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {

        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();

        itemWorld.SetItem(item);
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }


    //funkcja wyrzucenia itemu po kliknieciu
   /* public void DropItem(Vector3 position,Item item)
    {
       
        ItemWorld itemWorld = SpawnItemWorld(position + randomDir * 5f, item);
        itemWorld.GetComponent<Rigidbody>().AddForce(randomDir *5f,ForceMode.Impulse);
        return itemWorld;
    }*/
}
