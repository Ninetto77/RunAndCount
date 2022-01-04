using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public GameObject CoinsSpawn;
    public int coinChance;

    private Vector3 move ;
    private bool isCoinsSpawn;

    void Start()
    {
        move = Vector3.forward;
        PowerUpController.CoinsPowerUpEvent += BlockCoinsSpawn;

        isCoinsSpawn = Random.Range(1, 100) <= coinChance;
        CoinsSpawn.SetActive(isCoinsSpawn);
    }

    
    void Update()
    {
        transform.Translate(move* Time.deltaTime * GameManager.Instance.CurrentMoveSpeed);
    }

    void BlockCoinsSpawn(bool active)
    {
        if (active)
        {
            CoinsSpawn.SetActive(true);
            return;
        }

        if (!isCoinsSpawn)
        {
            CoinsSpawn.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        PowerUpController.CoinsPowerUpEvent -= BlockCoinsSpawn;
    }
}
