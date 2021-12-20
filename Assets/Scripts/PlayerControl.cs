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
    [SerializeField] private PlayerHUD playerHud;
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
        ItemWorld.SpawnItemWorld(new Vector3(-29, 2, 0), new Item { itemType = Item.ItemType.SpeedPlus, amount = 7 });
        ItemWorld.SpawnItemWorld(new Vector3(-30, 2, 0), new Item { itemType = Item.ItemType.Coin, amount = 3 });
        ItemWorld.SpawnItemWorld(new Vector3(-24, 2, 0), new Item { itemType = Item.ItemType.SpeedPlus, amount = 7 });
        ItemWorld.SpawnItemWorld(new Vector3(-20, 2, 0), new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(-28, 2, 0), new Item { itemType = Item.ItemType.DamagePlus, amount = 7 });


    }

    //zbieranie itemów na trigger
    private void OnTriggerEnter(Collider other)
    {   //zbieranie przedmiotów
        ItemWorld iWorld = other.gameObject.GetComponent<ItemWorld>();
        if (iWorld!= null)
        {   //dodawanie ich do invertory
            inventory.AddItem(iWorld.GetItem());
            if (playerStats.quest.isActive)
            {   //jesli quest jest aktywny dodawnie postêpów i czy jest ukonczony
                playerStats.quest.goal.CollectItems(iWorld.GetItem().itemType.ToString(), iWorld.GetItem().amount);
                if (playerStats.quest.goal.IsComplited())
                {   //jesli zbierzemy odpowiednia ilosc itemow koczmy questa, dostajemy exp, golda i oznaczamy jako nieaktywnego
                    playerStats.GetExp(playerStats.quest.exp);
                    playerStats.GetGold(playerStats.quest.gold);
                    playerStats.quest.Compleated();
                }
            }
            iWorld.DestroySelf();
            //ui_inventory.RefreshInventory();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

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

        //otwieranie inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            playerHud.OpenInventory();
        }

    }
}
