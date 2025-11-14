using UnityEngine;

public class CatManagerScript : MonoBehaviour
{
    public GameObject ClawSpawnerBrain;
    public GameObject Yarns;
    public GameObject CatTree;
    public GameObject CatBed;

    public ClawSpawner[] spawners;
    public float breakTime;            // the amount of time in between each spawner being activated.
    public float stageTime;             // the amount of time for which this phase will last
    public float activeTime;            // the amount of time that a single spawner can be active
    public float timer;
    public float overallTimer;

    public bool isActive = false;

    void Start()
    {
       
    }

    // for this script, it basically only selects ONE spawner to be active at any given time! and it's randomised (i.e. which spawner is used is different each time yay)
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        overallTimer += Time.deltaTime;


        if (!isActive && timer > breakTime)                 // no spawners active + break time is up = set a spawner to be active
        {
            int MyIndex = Random.Range(0, spawners.Length); 
            spawners[MyIndex].gameObject.SetActive(true);
            spawners[MyIndex].scratch();
            isActive = true;
        }
        else if (isActive && timer > activeTime + breakTime)    // a spawner is active but it has exceeded its active time = shut it down and reset.
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].gameObject.SetActive(false);
            }
            isActive = false;
            timer = 0f;
        }


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