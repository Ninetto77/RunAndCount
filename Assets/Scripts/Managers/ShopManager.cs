using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public List<ShopItem> Items;
    public ShopItem.ItemType ActivateSkin;

    public void CheckItemButtons()
    {
        foreach (ShopItem item in Items)
        {
            item.CheckButtons();
        }
    }
}
