using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemType itemType;
    public int amount;

    //typy itemów w inventory
    public enum ItemType
    {
        Coin,
        HealthPotion,
        ManaPotion,
        DamagePlus,
        SpeedPlus,
    }


    //Przypisanie sprite do typu itemu
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin: return ItemAssets.Instance.CoinSprite;
            case ItemType.DamagePlus: return ItemAssets.Instance.DamagePlusSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.HealthPotionSprite;
            case ItemType.ManaPotion: return ItemAssets.Instance.ManaPotionSprite;
            case ItemType.SpeedPlus: return ItemAssets.Instance.SpeedPlusSprite;
        }
    }

}
