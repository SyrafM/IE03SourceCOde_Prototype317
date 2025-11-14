using UnityEngine;

public class DmgTileScript : MonoBehaviour
{
    public int damage;
    public float telegraphTime;
    public float lifetime;

    private float timer;
    private SpriteRenderer sr;
    private Collider2D col;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        col.enabled = false;
        
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= telegraphTime)
        {
            if (sr != null)
            {
                col.enabled = true;
            }
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
    }
}
