using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
     
       
        Debug.Log(itemList.Count);
    }

    //dodanie item�w do listy
    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    //zwr�cenie listy item�w 
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
