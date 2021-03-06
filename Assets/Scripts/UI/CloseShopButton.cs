using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseShopButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(CloseShopWindow);
    }
    public void CloseShopWindow()
    {
        GameManager.Instance.CloseShopWindow();
    }
}
