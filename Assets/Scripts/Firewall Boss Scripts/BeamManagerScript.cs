using UnityEngine;

public class BeamManagerScript : MonoBehaviour
{
    public GameObject BeamSpawnerBrain;

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
            overallTimer = 0;
            // idk add in transition here?
        }
        
    }
}
