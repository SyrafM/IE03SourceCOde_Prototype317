using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    public int damage;
    public GameObject bulletPrefab;

    //split stuff
    public int splitCount = 2;
    public float splitAngle = 30f; // dets how wide the bullets spread when they split
    public float splitSpeedMultiplier = 0.8f; //make them a bit slower after they split.
    public int maxSplitDepth = 2; // denotes no. of times the bullet can split
    public int currentDepth = 0; // denotes the current generation of the bullet

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
            StartCoroutine(ReenableCollider(col, 0.05f));
        }
        if (target != null && currentDepth == 0)
        {
            // the bullet will move towards the player's position at spawn
            // only applies to the first bullet spawned
            Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
            bulletRB.linearVelocity = moveDir;
        }

        // this destroys the bullet after 3 seconds
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        // to rotate the sprite to fix the weird angle
        if (bulletRB.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(bulletRB.linearVelocity.y, bulletRB.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 45f);
            // adjust angle as necessary
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Walls")
        {
            if (currentDepth < maxSplitDepth)
            {
                SplitBullets();

            }
            Destroy(gameObject);
        }
    }

    private void SplitBullets()
    {
        if (bulletRB == null || bulletPrefab == null)
        {
            return;
        }

        Vector2 currentDir = bulletRB.linearVelocity.normalized;
        float baseAngle = Mathf.Atan2(currentDir.y, currentDir.x) * Mathf.Rad2Deg;

        // evenly spread bullets across the splitAngle cone
        float totalSpread = splitAngle * (splitCount - 1);
        float startAngle = baseAngle - totalSpread / 2f;

        for (int i = 0; i < splitCount; i++)
        {
            // float angleOffset = (i == 0 ? -splitAngle : splitAngle);
            // float newAngle = baseAngle + angleOffset;

            float newAngle = startAngle + (splitAngle * i);

            Vector2 newDir = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

            Vector2 spawnPos = (Vector2)transform.position + newDir * 0.3f;                             // offset spawn slightly to prevent immediate splitting due to collisions

            GameObject newBullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

            BulletScript newBulletScript = newBullet.GetComponent<BulletScript>();
            newBulletScript.speed = speed * splitSpeedMultiplier;
            newBulletScript.damage = damage;
            newBulletScript.currentDepth = currentDepth + 1;
            newBulletScript.maxSplitDepth = maxSplitDepth;

            Rigidbody2D newRb = newBullet.GetComponent<Rigidbody2D>();
            newRb.linearVelocity = newDir * speed * splitSpeedMultiplier;

            //fix angle
            float angle = Mathf.Atan2(bulletRB.linearVelocity.y, bulletRB.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 45f);

            Collider2D col = newBullet.GetComponent<Collider2D>();
            if (col != null && newBulletScript != null)
            {
                col.enabled = false;
                Debug.Log("collision disabled");
                newBulletScript.StartCoroutine(newBulletScript.ReenableCollider(col, 0.1f)); // start coroutine on the new bullet 
            }
        }
    }
    private System.Collections.IEnumerator ReenableCollider(Collider2D col, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (col != null)
        {
            col.enabled = true;
        }
    }

}