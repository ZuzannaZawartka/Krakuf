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
            QuestProgress(iWorld.GetItem().itemType.ToString(), iWorld.GetItem().amount);
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

    void QuestProgress(string itemtype, int amount) 
    {
        if (playerStats.quests.Count > 0)
        {
            for (int i = 0; i < playerStats.quests.Count; i++)
            {
                for (int j = 0; j < playerStats.quests[i].largeQuest.Count; j++)
                {
                    if (playerStats.quests[i].largeQuest[j].isActive)
                    {
                        playerStats.quests[i].largeQuest[j].goal.CollectItems(itemtype, amount);
                        if (playerStats.quests[i].largeQuest[j].goal.IsComplited())
                        {
                            playerStats.GetExp(playerStats.quests[i].largeQuest[j].exp);
                            playerStats.GetGold(playerStats.quests[i].largeQuest[j].gold);
                            playerStats.quests[i].largeQuest[j].Compleated();

                            if (j == playerStats.quests[i].largeQuest.Count - 1)
                                playerStats.quests.Remove(playerStats.quests[i]);
                            else
                                playerStats.quests[i].largeQuest[j + 1].isActive = true;

                            if (i >= 0 && playerStats.quests.Count > 0)
                                i--;
                        }
                        break;
                    }
                }
            }
        }
    }
}
