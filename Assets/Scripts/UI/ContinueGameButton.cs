using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ContinueGame);
    }
    public void ContinueGame()
    {
        GameManager.Instance.PauseGame(false);
    }
}
