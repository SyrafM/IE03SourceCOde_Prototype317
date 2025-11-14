using UnityEngine;

public class HandSpawner : MonoBehaviour
{
    public GameObject handPrefab;
    // public Transform handSpawnPos;
    public Vector2 spawnRight;
    public Vector2 spawnLeft;

    private float timer;
    private float breakTime;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            timer = 0f;
            sweepHand();
        }
    }

    public void sweepHand()
    {
        Vector2 spawnPos;
        int direction;
        if (Random.Range(0,2) == 0)
        {
            spawnPos = spawnRight;
            direction = -1;
        } else
        {
            spawnPos = spawnLeft;
            direction = 1;
        }
        GameObject hand = Instantiate(handPrefab, spawnPos, Quaternion.identity);
        Animator handAnimator = hand.GetComponent<Animator>();

        if (direction == -1)
        {
            handAnimator.SetBool("SwipeRight", true);
        } else
        {
            handAnimator.SetBool("SwipeRight", false);
        }
        hand.GetComponent<HandScript>().SetDirection(direction);
    }
}
