using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    public enum GameTypes
    {
        Play,
        Menu,
        Finish
    }
public class GameManager : Singleton<GameManager>
{
    public PlayerController _player;
    public RoadSpawner _roadSpawner;
    public float MoveSpeed;
    public List<Skin> Skins;


    [Header("UI")]
    public DisplayedUIPanel LoseGameWindow;
    public DisplayedUIPanel ShopWindow;
    public DisplayedUIPanel GameWindow;

    [Header("Text UI in Game")]
    public Text CoinsTxt;
    public Text PlayerPointsTxt;
    public Text CountTxt;
    private Text BestCountTxt;

    [Header("Text UI Loose")]
    public Text CoinsLoseTxt;
    public Text PlayerPointsLoseTxt;

    [Header("Text UI in Shop")]
    public Text CoinsShopTxt;

    [ReadOnly]
    public float Coins;
    public GameTypes GameType = GameTypes.Menu;

    void Update()
    {
        if (! _player)
        {
            return;
        }

        if (GameType != GameTypes.Play)
        {
            return;
        }

        CountTxt.text = ((int)_player.Points).ToString();
        MoveSpeed -= .1f * Time.deltaTime;
        MoveSpeed = Mathf.Clamp(MoveSpeed, -10, -20);
    }

    public void IncreaseCoins()
    {
        Coins++;
        RefreshCoins();
    }

    public void UpdatePointsTxt()
    {
        PlayerPointsTxt.text = _player.PlayerPoints.ToString();
    }

    public void StartGame()
    {
        GameType = GameTypes.Play;
        _roadSpawner.StartGame();
        LoseGameWindow.ClosePanel();
        Time.timeScale = 1;
        UpdatePointsTxt();
    }

    public void FinishGame()
    {
        GameType = GameTypes.Finish;
        LoseGameWindow.OpenPanel();
        Time.timeScale = 0;
        //BestCountTxt.text = _player.BestCountValue.ToString();
        RefreshCoins();
    }

    public void CloseShopWindow()
    {
        ShopWindow.ClosePanel();
        GameWindow.OpenPanel();
        GameType = GameTypes.Play;
    }

    public void OpenShopWindow()
    {
        ShopWindow.OpenPanel();
        GameWindow.ClosePanel();
        ShopManager.Instance.CheckItemButtons();
        RefreshCoins();
    }

    public void RefreshCoins()
    {
        CoinsShopTxt.text = Coins.ToString();
        CoinsTxt.text = Coins.ToString();
        CoinsLoseTxt.text = Coins.ToString();
    }

    public void ActiveSkin(int skinIndex)
    {
        foreach (var skin in Skins)
        {
            skin.HideSkin();
        }
        Skins[skinIndex].ShowSkin();
    }
}
