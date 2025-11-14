using UnityEngine;

public class YarnManagerScript : MonoBehaviour
{
    public float stageTime;
    public float overallTimer;
    public float timer;                 // idk if this is need WIP

    public GameObject Yarns;
    public GameObject Cat;
    public GameObject CatTree;
    public GameObject CatBed;

    // Update is called once per frame
    void Update()
    {
        overallTimer += Time.deltaTime;

        if (overallTimer > stageTime)
        {
            Yarns.SetActive(false);
            CatTree.SetActive(false);
            CatBed.SetActive(false);
            Cat.SetActive(true);
            overallTimer = 0;
        }
    }
}
