using UnityEngine;

public class HandScript : MonoBehaviour
{
    public float speed = 10f;
    public float curveSpeed = 60f; // degrees per second
    public int damage;
    public float lifetime = 3f;
    public float telegraphTime = 1.5f;

    private SpriteRenderer sr;
    private Collider2D col;
    private Color originalColor;
    private Rigidbody2D rb;
    private int direction = 1; // 1 = right, -1 = left
    private float timer;
    private bool canMove = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void SetDirection(int dir)
    {
        direction = dir;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        originalColor = sr.color;
        col.enabled = false;
        rb = GetComponent<Rigidbody2D>();

        // start as faded pink to telegraph that an attack will be happening
        if (sr != null)
        {
            sr.color = new Color(1f, 0f, 0f, 0.3f);
        }
        rb.linearVelocity = Vector2.zero;

        // this is for the sprite when it is madeee
        /**
        if (direction == -1)
        {
            transform.localScale = new Vector2(-1 * (transform.localScale.x), transform.localScale.y);
        }
        **/
        // rb.linearVelocity = transform.right * speed; // initial direction (right)
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (!canMove && timer > telegraphTime)
        {
            canMove = true;

            // turn fully visible/red/solid when activated
            if (sr != null)
            {
                col.enabled = true;
                sr.color = originalColor;
                audioManager.PlaySFX(audioManager.bossAttackTwo);
            }

            Vector2 moveDir = (direction == 1) ? Vector2.right : Vector2.left;
            rb.linearVelocity = moveDir * speed;
        }
        
        if (canMove)
        {
            float curveAmount = curveSpeed * Time.fixedDeltaTime * direction;
            rb.linearVelocity = Quaternion.Euler(0, 0, curveAmount) * rb.linearVelocity;
            // rb.linearVelocity = Quaternion.Euler(0, 0, curveSpeed * Time.fixedDeltaTime) * rb.linearVelocity;

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
