using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenShopButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenShopWindow);
    }
    public void OpenShopWindow()
    {
        Debug.Log("Open");
        GameManager.Instance.OpenShopWindow();
    }
}
