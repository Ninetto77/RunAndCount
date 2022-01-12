using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevelButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Restart);
    }
    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.StartGame();
    }
}
