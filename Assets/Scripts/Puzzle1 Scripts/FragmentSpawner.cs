using UnityEngine;

public class FragmentSpawner : MonoBehaviour
{
    public GameObject fragmentPrefab;
    public Vector2 spawnMin;
    public Vector2 spawnMax;
    public int spawnCount;

    void Start()
    {
        spawnFragment();
    }

    public void spawnFragment()
    {
        if (spawnCount < 8)
        {
            Vector2 randomPos = new Vector2(Random.Range(spawnMin.x, spawnMax.x), Random.Range(spawnMin.y, spawnMax.y));
            Instantiate(fragmentPrefab, randomPos, Quaternion.identity);
            spawnCount = spawnCount + 1;
        }
    }
    void OnEnable()
    {
        FragmentScript.OnFragmentCollect += OnFragmentCollected;
    }
    void OnDisable()
    {
        FragmentScript.OnFragmentCollect -= OnFragmentCollected;
    }
    void OnFragmentCollected(int worth)
    {
        spawnFragment();
    }
}
