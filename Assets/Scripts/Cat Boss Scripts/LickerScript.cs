using System.Threading;
using UnityEngine;

public class LickerScript : MonoBehaviour
{
    public GameObject tongue;
    public Vector2 spawnMin;
    public Vector2 spawnMax;
    public float breakTime;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > breakTime)
        {
            timer = 0f;
            Lick();
        }
    }

    public void Lick()
    {
        Vector2 spawnPos;
        float randomX = Random.Range(spawnMin.x, spawnMax.x);
        float randomY = Random.Range(spawnMin.y, spawnMax.y);
        spawnPos = new Vector2(randomX, randomY);

        GameObject obj = Instantiate(tongue, spawnPos, Quaternion.identity);

        TongueScript tongueScript = obj.GetComponent<TongueScript>();
        
        if (randomX < (spawnMax.x + spawnMin.x) / 2)
        {
            tongueScript.leftSpawner = true;
        }
        else
        {
            tongueScript.leftSpawner= false;
        }

    }
}
