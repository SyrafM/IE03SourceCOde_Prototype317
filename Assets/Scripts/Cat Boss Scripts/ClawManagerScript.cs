using System.Threading;
using UnityEngine;

public class ClawManagerScript : MonoBehaviour
{
    public GameObject ClawSpawnerBrain;
    public GameObject Yarns;
    public GameObject CatTree;
    public GameObject CatBed;

    public float breakTime;
    public float stageTime;
    public float activeTime;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    public float timer;
    public float overallTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        overallTimer += Time.deltaTime;

        if (overallTimer > stageTime)
        {
            ClawSpawnerBrain.SetActive(false);
            Yarns.SetActive(true);
            CatTree.SetActive(true);
            CatBed.SetActive(false);
            overallTimer = 0f;
        }
    }
}
