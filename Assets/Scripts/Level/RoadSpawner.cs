using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] RoadBlockPrefabs;
    public GameObject StartBlock;

    private Vector3 startPosition;

    private float currentblockZPos;
    private float startblockZPos;
    private float blockLength = 0 ;
    private int blockCount = 7;

    public Transform PlayerTransform;
    List<GameObject> CurrentBlocks = new List<GameObject>();
    


    void Start()
    {
        startblockZPos = StartBlock.transform.position.z;
        blockLength = StartBlock.gameObject.GetComponent<BoxCollider>().bounds.size.z;
        startPosition = PlayerTransform.position;
        //Destroy(StartBlock);

        StartGame();

    }

    public void StartGame()
    {
        currentblockZPos = startblockZPos;
        PlayerTransform.position = startPosition;
        Destroy(StartBlock);

        var _player = PlayerTransform.gameObject.GetComponent<PlayerController>();
        if (_player)
        {
            _player.PlayerPoints = 1;
            GameManager.Instance.Points = 0;
            GameManager.Instance.PointMultiplier = 1;
        }


        foreach (var block in CurrentBlocks)
        {
            Destroy(block);
        }
        CurrentBlocks.Clear();

        for (int i = 0; i < blockCount; ++i)
        {
            SpawnBlock();
        }
    }

    private void SpawnBlock()
    {
        GameObject block = Instantiate(RoadBlockPrefabs[UnityEngine.Random.Range(0, RoadBlockPrefabs.Length)], transform);
        Vector3 blockPos;

        if (CurrentBlocks.Count > 0)
        {
            blockPos = CurrentBlocks[CurrentBlocks.Count - 1].transform.position + new Vector3(0, 0, blockLength);
        }
        else
        {
            blockPos = new Vector3(0, 0, startblockZPos);
        }
       
        block.transform.position = blockPos;
        CurrentBlocks.Add(block);
    }

    void LateUpdate()
    {
        CheckForSpawn();
    }

    private void CheckForSpawn()
    {
        if (CurrentBlocks[0].transform.position.z - PlayerTransform.position.z < -40)
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        Destroy(CurrentBlocks[0].gameObject);
        CurrentBlocks.RemoveAt(0);
    }
}
