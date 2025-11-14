using UnityEngine;
using UnityEngine.UIElements;

public class BotSpawner : MonoBehaviour
{
    public GameObject botPrefab;
    public Vector2 spawnMin;
    public Vector2 spawnMax;
    public int spawnCount;
    public BotPuzzleController controller;
    public int botsKilled = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnBot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBot()
    {
        if (spawnCount < 8)
        {
            Vector2 randomPos = new Vector2(Random.Range(spawnMin.x, spawnMax.x), Random.Range(spawnMin.y, spawnMax.y));

            Vector3 spawnPos = new Vector3(randomPos.x, randomPos.y, -6);           // CHANGE THE -6 IF THE LAYERS ARE MESSING UP
            Instantiate(botPrefab, spawnPos, Quaternion.identity);
            spawnCount = spawnCount + 1;
        }
    }

    public void OnBotKilled()
    {
        botsKilled++;

        if (controller != null)
        {
            controller.UpdateProgress(botsKilled);
        }
        spawnBot();
    }
}
