using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Point;
    private PlayerController _playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerController = other.GetComponent<PlayerController>();
            if (_playerController)
            {
                if (_playerController.CanBeChange)
                {
                    _playerController.PlayerPoints += Point;
                    _playerController.FobidToDamage();
                    GameManager.Instance.UpdatePointsTxt();
                }
            }
        }
    }
}
