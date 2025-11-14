using UnityEngine;
using UnityEngine.VFX;

public class FirewallPhaseManager : MonoBehaviour
{
    public GameObject BeamSpawner; // phase 0
    public GameObject BulletSpawner; // phase 1
    public GameObject HandSpawner; // phase 2
    public GameObject HellSpawner; // phase 3
    public GameObject Firewall;

    // public GameObject HoleSpawner; // uh rn its only activated if it reaches half health, but it should be deactivated when beam spawner is happening.

    /**
    public float BeamTime;
    public float BulletTime;
    public float HandTime;
    public float HellTime;
    **/

    public float phaseDuration = 5f;

    private float timer;
    private int currentPhase = -1;
    private GameObject[] spawners;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawners = new GameObject[] { BeamSpawner, BulletSpawner, HandSpawner, HellSpawner };

        SwitchPhase(Random.Range(0, spawners.Length));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= phaseDuration)
        {
            timer = 0;

            int nextPhase;
            do
            {
                nextPhase = Random.Range(0, spawners.Length);
            }
            while (nextPhase == currentPhase); // to avoid repeating the same phase twice in a row

            SwitchPhase(nextPhase);
        }
   
    }
    private void SwitchPhase(int newPhase)
    {
        currentPhase = newPhase;
        for (int i = 0; i < spawners.Length; i++)
        {
            if (spawners[i] != null)
            {
                spawners[i].SetActive(i == newPhase);       // i == newPhase is a bool so it can be passed directly then if its the new phase it will register as true
            }
        }

        // while the beam spawner is active, the firewall is temporarily inactive
        if (Firewall != null)
        {
            bool beamActive = (newPhase == 0);
            Firewall.SetActive(!beamActive);
        }
        Debug.Log($"Switched to phase {newPhase}");

    }
}
