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
    [SerializeField] private GameObject itemSlotTemplateG;
    [SerializeField] private PlayerHUD playerHud;
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
       //RefreshInventory();
    }

    public void RefreshInventory()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = -2;
        int y = 0;
        float itemSlotCellSize = 200;
        //generowanie template dla itemów w inventory 
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate,itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiT = itemSlotRectTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI uiType = itemSlotRectTransform.Find("type").GetComponent<TextMeshProUGUI>();
            uiType.SetText(item.itemType.ToString());
            if (item.amount > 1)
            {
                uiT.SetText(item.amount.ToString());
            }
            else
            {
                uiT.SetText(" ");
            }
            x++;
        }
    }
}
