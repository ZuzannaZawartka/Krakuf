using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform inventoryContainer;
    private Transform itemContainer;

    private void Start()
    {
        inventoryContainer = transform.Find("InventoryS");
        itemContainer = inventoryContainer.Find("item_bg");

    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        int x = -2;
        int y = 1;
        float itemSlotCellSize = 100;
        //generowanie template dla itemów w inventory 
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemContainer, inventoryContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            Image image = itemSlotRectTransform.Find("item").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x >= 3)
            {
                x = -2;
                y--;
            }


        }
    }
}
