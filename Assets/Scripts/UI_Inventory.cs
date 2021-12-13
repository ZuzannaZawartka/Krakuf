using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private PlayerControl player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer(PlayerControl player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
       RefreshInventory();
    }

    public void RefreshInventory()
    {
        int x = -2;
        int y = 1;
        float itemSlotCellSize = 110;
        //generowanie template dla itemów w inventory 
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate,itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiT = itemSlotRectTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiT.SetText(item.amount.ToString());
            }
            else
            {
                uiT.SetText("");
            }
            x++;
            if (x >= 3)
            {
                x = -2;
                y--;
            }


        }
    }
}
