using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int Damage;
    private PlayerController _playerController;
    private Vector3 enemyVector;
    private Vector3 playerPosition;
    private Vector3 currentPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerController = other.GetComponent<PlayerController>();
            if (_playerController)
            {
                if (_playerController.CanBeChange)
                {
                    _playerController.PlayerPoints -= Damage;
                    _playerController.FobidToDamage();
                    GameManager.Instance.UpdatePointsTxt();
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Start()
    {
        enemyVector = playerPosition - currentPosition;
    }
    public void Update()
    {
        if (enemyVector.z < 5f)
        {
            //Debug.Log("MOVE");
            //MoveToPlayer();
        }
    }

    public void MoveToPlayer()
    {
        transform.Translate(enemyVector);
    }
}
