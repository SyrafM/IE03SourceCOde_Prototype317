using System.Collections;
using UnityEngine;

public class ClawSpawner : MonoBehaviour
{
    public GameObject claw;
    public Transform clawPos;
    public Animator catAnimator;

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

        catAnimator.SetBool("isScratch", true);

        // so this takes the scratch script component that's attached to the new prefab instance
        /*
        if (scratchScript != null)                                                      // assigns the direction value from the ClawSpawner script (i.e. the one in the inspector if its ticked) to the scratch script so the scratch script knows the direction
        {
            scratchScript.leftSpawner = leftSpawner;
        }
        */
    }

    private IEnumerator ResetScratchTarget()
    {
        yield return new WaitForSeconds(1f);
        hasScratchTarget = false;
        catAnimator.SetBool("isScratch", false);
    }

    
}