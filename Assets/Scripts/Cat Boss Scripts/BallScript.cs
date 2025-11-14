using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float speed;
    public int damage;
    public float telegraphTime;
    public float lifetime;

    private float timer;
    private bool canMove = false;
    private SpriteRenderer sr;
    private Collider2D col;
    private Color originalColor;

    public bool leftSpawner;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        originalColor = sr.color;

        col.enabled = false;

        // start as faded red to telegraph that an attack will be happening
        if (sr != null)
        {
            sr.color = new Color(1f, 0f, 0f, 0.3f);
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!canMove && timer >= telegraphTime)
        {
            canMove = true;

            // turn fully visible/red/solid when activated
            if (sr != null)
            {
                col.enabled = true;
                sr.color = originalColor;
            }
        }

        if (canMove)
        {
            // this if-else helps to check if the spawner is on the left of the map based on what was inputted in the inspector under the ClawSpawner component of each ClawSpawner game objects.
            // if the spawner is on the left side, then it will tell the spawner to make the laser go to the right.
            // else, it will tell the laser to go to the right. i hope this makes sense
            if (leftSpawner)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
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
