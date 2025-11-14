using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tile;

    // coordinate madness. insert in game inspector.
    public float minX;
    public float maxX;
    public float stepX = 1.2f;

    public float minY;
    public float maxY;
    public float stepY = 1.2f;

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
            spawnTile();
            spawnTile();
            spawnTile();
            spawnTile();
        }
    }

    public void spawnTile()
    {
        int columns = Mathf.FloorToInt((maxX - minX) / stepX) + 1;
        int rows = Mathf.FloorToInt((maxY - minY) / stepY) + 1;

        int randomCol = Random.Range(0, columns);
        int randomRow = Random.Range(0, rows);

        float spawnX = minX + randomCol * stepX;
        float spawnY = minY + randomRow * stepY;

        Vector2 spawnPos = new Vector2(spawnX, spawnY);
        Instantiate(tile, spawnPos, Quaternion.identity);
    }
}
