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
    public float CurrentMoveSpeed;
    public float BaseMoveSpeed;
    public List<Skin> Skins;

    [Header("Points")]
    public float Points;
    public float PointBaseValue;
    public float PointMultiplier;
    [ReadOnly]
    public float PowerUpMultiplier = 1;

    [Header("UI")]
    public DisplayedUIPanel MainMenuWindow;
    public DisplayedUIPanel LoseGameWindow;
    public DisplayedUIPanel ShopWindow;
    public DisplayedUIPanel GameWindow;
    public DisplayedUIPanel PauseWindow;


    [Header("Text UI in Game")]
    public Text CoinsTxt;
    public Text PlayerPointsTxt;
    public Text CountTxt;


    [Header("Text UI Loose")]
    public Text CoinsLoseTxt;
    public Text PointsLoseTxt;
    public Text BestCountTxt;

    [Header("Text UI in Shop")]
    public Text CoinsShopTxt;

    [Header("Text UI in Pause")]
    public Text CoinsPauseTxt;

    [ReadOnly]
    public float CurrentCoins;
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

        Points += PointBaseValue * PointMultiplier * PowerUpMultiplier * Time.deltaTime;
        PointMultiplier += 0.05f * Time.deltaTime;
        PointMultiplier = Mathf.Clamp(PointMultiplier, 1, 10);

        CountTxt.text = ((int)Points).ToString();
        CurrentMoveSpeed -= .1f * Time.deltaTime;
        CurrentMoveSpeed = Mathf.Clamp(CurrentMoveSpeed, -20, -10);
    }

    public void IncreaseCoins()
    {
        CurrentCoins++;
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
        PauseWindow.ClosePanel();
        MainMenuWindow.ClosePanel();
        GameWindow.OpenPanel();
        Time.timeScale = 1;
        UpdatePointsTxt();
        RefreshCoins();

        CurrentMoveSpeed = BaseMoveSpeed;
        Coins = PlayerPrefs.GetFloat("Coins");
    }

    public void PauseGame(bool IsPaused)
    {
        if (IsPaused)
        {
            GameType = GameTypes.Menu;
            PauseWindow.OpenPanel();
            Time.timeScale = 0;
        }
        else
        {
            GameType = GameTypes.Play;
            PauseWindow.ClosePanel();
            Time.timeScale = 1;
        }
    }


    public void FinishGame()
    {
        GameType = GameTypes.Finish;
        LoseGameWindow.OpenPanel();
        GameWindow.ClosePanel();
        Time.timeScale = 0;
        BestCountTxt.text = "Best Count: " + ((int)PlayerPrefs.GetFloat("BestCount")).ToString();
        PointsLoseTxt.text = "Current Count: " + ((int)Points).ToString();

        RefreshCoins();
        Coins = PlayerPrefs.GetFloat("Coins") + CurrentCoins; 
        PlayerPrefs.SetFloat("Coins", Coins);
    }

    public void OpenMainMenuGame()
    {
        GameType = GameTypes.Menu;
        LoseGameWindow.ClosePanel();
        PauseWindow.ClosePanel();
        GameWindow.ClosePanel();
        MainMenuWindow.OpenPanel();
        Time.timeScale = 0;
    }

    public void CloseShopWindow()
    {
        ShopWindow.ClosePanel(); 
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
        CoinsTxt.text = CurrentCoins.ToString();
        CoinsLoseTxt.text = CurrentCoins.ToString();
        CoinsPauseTxt.text = CurrentCoins.ToString();
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
