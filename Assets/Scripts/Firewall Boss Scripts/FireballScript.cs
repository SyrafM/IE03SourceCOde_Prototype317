using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float speed;
    Rigidbody2D bulletRB;
    public int damage;
    void Update()
    {
        // rotates to fix the sprite direction 
        if (bulletRB.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(bulletRB.linearVelocity.y, bulletRB.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 45f);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
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
    }
}
