using UnityEngine;

public class BotMovementScript : MonoBehaviour, IDamageable
{
    public int maxHealth = 3;
    public float roamSpeed = 5.0f;
    public Vector2 roamMin;
    public Vector2 roamMax;
    public BotSpawner spawner;

    private Rigidbody2D rb;
    private int currentHealth;
    private Vector2 roamTarget;
    private bool hasRoamTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spawner = FindFirstObjectByType<BotSpawner>();
    }

    void Update()
    {
        Roam();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            if (spawner != null)
            {
                spawner.OnBotKilled();
            }
            Destroy(gameObject);   // enemy is dead bruh
        }
    }

    private void Roam()
    {
        if (!hasRoamTarget)
        {
            float randomX = Random.Range(roamMin.x, roamMax.x);
            float randomY = Random.Range(roamMin.y, roamMax.y);
            roamTarget = new Vector2(randomX, randomY);
            hasRoamTarget = true;
        }

        Vector2 moveDir = (roamTarget - (Vector2)transform.position).normalized * roamSpeed;
        rb.linearVelocity = moveDir;

        if (Vector2.Distance(transform.position, roamTarget) < 0.5f)
        {
            hasRoamTarget = false;
        }
    }

    // this part is for debugging - it shows the area that the cat can roam in ig
    void OnDrawGizmos()
    {
        // draws the current roam target
        if (hasRoamTarget)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(roamTarget, 0.2f);

            // draws a line from cat to target
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, roamTarget);
        }
    }

}
