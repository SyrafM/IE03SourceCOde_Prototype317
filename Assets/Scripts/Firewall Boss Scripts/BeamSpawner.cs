using UnityEngine;
using System.Collections;

public class BeamSpawner : MonoBehaviour
{
    public GameObject beam;
   // public Transform beamPos;
    private BeamManagerScript beamManager;

    private float timer;
    private float breakTime;
    private Vector2 beamTarget;
    private bool hasBeamTarget;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // public bool leftSpawner;
    void Start()
    {
        beamManager = GetComponentInParent<BeamManagerScript>();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log("Timer: " + timer);

        if (timer > beamManager.breakTime)
        {
            timer = 0f;
            shootBeam();
            // Debug.Log("Scratching!");

        }
    }
    
    public void shootBeam()
    {
        if (!hasBeamTarget)
        {
            float randomX = Random.Range(beamManager.minX, beamManager.maxX);
            float randomY = Random.Range(beamManager.minY, beamManager.maxY);
            beamTarget = new Vector2(randomX, randomY);
            hasBeamTarget = true;
            StartCoroutine(ResetBeamTarget());
        }
        GameObject obj = Instantiate(beam, beamTarget, Quaternion.identity);      // creates a clone of the prefab
        // audioManager.PlaySFX(audioManager.bossAttackThree);
        // BeamScript beamScript = obj.GetComponent<beamScript>();                // so this takes the scratch script component that's attached to the new prefab instance

        /**
        if (scratchScript != null)                                                      // assigns the direction value from the ClawSpawner script (i.e. the one in the inspector if its ticked) to the scratch script so the scratch script knows the direction
        {
            scratchScript.leftSpawner = leftSpawner;
        }
        **/

    }

    private IEnumerator ResetBeamTarget()
    {
        yield return new WaitForSeconds(breakTime);
        hasBeamTarget = false;

    }
}
