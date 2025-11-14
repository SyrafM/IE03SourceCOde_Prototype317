using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;
    public Transform ballPos;

    private float timer;

    public bool leftSpawner;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log("Timer: " + timer);

        if (timer > 3)
        {
            timer = 0f;
            spawn();
            // Debug.Log("Shooting!");

        }
    }

    public void spawn()
    {
        GameObject obj = Instantiate(ball, ballPos.position, Quaternion.identity);      // creates a clone of the prefab
        BallScript ballscript = obj.GetComponent<BallScript>();                // so this takes the ball script component that's attached to the new prefab instance
        if (ballscript != null)                                                      // assigns the direction value from the yarnspawner script (i.e. the one in the inspector if its ticked) to the scratch script so the scratch script knows the direction
        {
            ballscript.leftSpawner = leftSpawner;
        }
        audioManager.PlaySFX(audioManager.bossAttackOne);


    }
}
