using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
   public enum ItemType
    {
        FIRST_SKIN,
        SECOND_SKIN,
        THIRD_SKIN,
    }

    public ItemType Type;
    public Button BuyButton;
    public Button ActivateButton;
    public float Cost;

    public bool IsBought;
    private bool IsActive
    {
        get
        {
            return Type == ShopManager.Instance.ActivateSkin;
        }
    }

  public void CheckButtons()
    {
        BuyButton.gameObject.SetActive(!IsBought);
        BuyButton.interactable = CanBuy() ;

        ActivateButton.gameObject.SetActive(IsBought);
        ActivateButton.interactable = !IsActive;
    }

    public bool CanBuy()
    {
        return GameManager.Instance.CurrentCoins >= Cost;
    }

    public void BuyItem()
    {
        if (!CanBuy())
        {
            return;
        }

        IsBought = true;
        GameManager.Instance.Coins -= Cost;
        PlayerPrefs.SetFloat("Coins", GameManager.Instance.Coins);
        CheckButtons();
        GameManager.Instance.RefreshCoins();
    }

    public void ActivateItem()
    {
        ShopManager.Instance.ActivateSkin = Type;
        ShopManager.Instance.CheckItemButtons();

        switch(Type)
        {
            case ItemType.FIRST_SKIN:
                GameManager.Instance.ActiveSkin(0);
                break;
            case ItemType.SECOND_SKIN:
                GameManager.Instance.ActiveSkin(1);
                break;
            case ItemType.THIRD_SKIN:
                GameManager.Instance.ActiveSkin(2);
                break;
        }
    }
}
