using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public float shootingRange;

    private float timer;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log("Timer: " + timer);

        if (timer > 2)
        {
            timer = 0f;
            shoot();
            // Debug.Log("Shooting!");

        }
    }

    void shoot()
    {
        audioManager.PlaySFX(audioManager.bossAttackOne);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}