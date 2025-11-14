using Unity.VisualScripting;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    public float telegraphTime;
    public float lifetime;
    public int damage;

    private float timer;
    private SpriteRenderer sr;
    private Color originalColor;
    private Collider2D col;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        col = GetComponent<Collider2D>();

        col.enabled = false;

        // start as faded red to telegraph that an attack will be happening
        if (sr != null)
        {
            sr.color = new Color(1f, 0.7f, 0.7f, 0.9f);
        }

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= telegraphTime)
        {
            // turn fully visible/red/solid when activated
            if (sr != null)
            {
                col.enabled = true;
                sr.color = originalColor;
                audioManager.PlaySFX(audioManager.bossAttackThree);
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
