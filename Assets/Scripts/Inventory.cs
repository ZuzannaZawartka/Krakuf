using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
   
    }

    //dodanie itemów do listy
    public void AddItem(Item item)
    {
       
        if (item.isStackable())
        {
            bool isInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                   inventoryItem.amount += item.amount;
                    isInInventory = true;
                }
            }
            if (!isInInventory)
            {
                itemList.Add(item);
            }
        }
        else if(!item.isStackable())
        {
            itemList.Add(item);
        }
    }

    //funkcja usuniecia itemu po wyrzuceniu z intentory
    /*public void RemoveItem(Item item) 
    {
        if (item.isStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory !=null && itemInInventory.amount <= 0)
            {
                itemList.Remove(item);
            }
        }
        else
        {
            itemList.Remove(item);
        }
    }*/

    //zwrócenie listy itemów 
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
