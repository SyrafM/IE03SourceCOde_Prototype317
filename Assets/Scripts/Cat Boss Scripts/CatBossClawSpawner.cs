using System.Collections;
using UnityEngine;

public class CatBossClawSpawner : MonoBehaviour
{
    public GameObject claw;
    public Transform clawPos;

    private ClawManagerScript clawManager;

    private float timer;
    private float breakTime;
    private Vector2 scratchTarget;
    private bool hasScratchTarget;
    AudioManager audioManager;


    // public bool leftSpawner;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        clawManager = GetComponentInParent<ClawManagerScript>();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log("Timer: " + timer);

        if (timer > 3)
        {
            timer = 0f;
            scratch();
            // Debug.Log("Scratching!");
        }

    }

    public void scratch()
    {
        if (!hasScratchTarget)
        {
            float randomX = Random.Range(clawManager.minX, clawManager.maxX);
            float randomY = Random.Range(clawManager.minY, clawManager.maxY);
            scratchTarget = new Vector2(randomX, randomY);
            hasScratchTarget = true;
            StartCoroutine(ResetScratchTarget());
        }
        GameObject obj = Instantiate(claw, scratchTarget, Quaternion.identity);      // creates a clone of the prefab
        ScratchScript scratchScript = obj.GetComponent<ScratchScript>();

        audioManager.PlaySFX(audioManager.bossAttackOne);
    }

    private IEnumerator ResetScratchTarget()
    {
        yield return new WaitForSeconds(breakTime);
        hasScratchTarget = false;
    }


}