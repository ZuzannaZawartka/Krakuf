using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpPower = 30.0f;
    public CharacterController player;
    public PlayerStats playerStats;
    public float gravity = -10;
    public bool sprint = false;
    private Inventory inventory;
    [SerializeField] private UI_Inventory ui_inventory;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        inventory = new Inventory();
        ui_inventory.SetPlayer(this);
        ui_inventory.SetInventory(inventory);
        ItemWorld.SpawnItemWorld(new Vector3(-27, 2, 0), new Item { itemType = Item.ItemType.Coin, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(-29, 2, 0), new Item { itemType = Item.ItemType.SpeedPlus, amount = 3 });


    }

    //zbieranie item�w na trigger
    private void OnTriggerEnter(Collider other)
    {
        ItemWorld iWorld = other.gameObject.GetComponent<ItemWorld>();
        if (iWorld!= null)
        {
            inventory.AddItem(iWorld.GetItem());
            iWorld.DestroySelf();
            ui_inventory.RefreshInventory();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Poruszanie sie WASD
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        player.Move(move * speed * Time.deltaTime);

        //Grawitacja
        if (player.isGrounded == false)
        {
            velocity.y += gravity * Time.deltaTime;
            player.Move(velocity * Time.deltaTime);
        }

        if (player.isGrounded)
            velocity.y = -1.0f;

        //Skok
        if (Input.GetKey(KeyCode.Space) && player.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
        }

        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.isGrounded && playerStats.currStamina > 10)
        {
            speed *= 2;
            sprint = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && sprint)
        {
            speed /= 2;
            sprint = false;
        }

    }
}
