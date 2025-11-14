using UnityEngine;

public class BulletHellSpawner : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public int bulletCount = 12;
    public float speed;

    private float timer;
    private float rotationOffset = 0f;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0f;
            rainHell();
        }
    }

    void rainHell()
    {
        float angleStep = 360f / bulletCount;
        // float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = rotationOffset + (i * angleStep);
            float dirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            Vector2 dir = new Vector2(dirX, dirY).normalized;

            GameObject b = Instantiate(bullet, bulletPos.position, Quaternion.identity);
            b.GetComponent<Rigidbody2D>().linearVelocity = dir * speed;

        }
        audioManager.PlaySFX(audioManager.bossAttackOne);
        rotationOffset += 10f;

    }
}
