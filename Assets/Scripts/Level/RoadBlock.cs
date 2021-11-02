using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public GameObject CoinsSpawn;
    public int coinChance;

    private Vector3 move ;

    void Start()
    {
        move = Vector3.forward;
        CoinsSpawn.SetActive(Random.Range(1, 100) <= coinChance);
    }

    
    void Update()
    {
        transform.Translate(move* Time.deltaTime * GameManager.Instance.MoveSpeed);
    }
}
