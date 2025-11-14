using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    public int damage;

    private Animator animator;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy");
        animator = GetComponent<Animator>();

        if (target != null)
        {
            // the bullet will move towards the enemy's position at spawn
            Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
            bulletRB.linearVelocity = moveDir;
        }

        // this destroys the bullet after 3 seconds
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.Log("Enemy has been hit!");

                // this part is to animate the bullet

                int randomIndex = Random.Range(0, 3);
                animator.SetInteger("ExplosionType", randomIndex);
                animator.SetTrigger("Explode");

                bulletRB.linearVelocity = Vector2.zero;
                GetComponent<Collider2D>().enabled = false;

                Destroy(gameObject, 0.45f); // rmb to adjust this according to the animation length
            }
            // below was before i implemented Idamageable. this code is redundant and slow, just left behind for completion sake
            /**
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            CatHealth catHealth = collision.gameObject.GetComponent<CatHealth>();
            FirewallHealth firewallHealth = collision.gameObject.GetComponent<FirewallHealth>();
            ChatLogHealthScript chatlogHealth = collision.gameObject.GetComponent<ChatLogHealthScript>();

            if (catHealth != null)
            {
                catHealth.TakeDamage(damage);
                Debug.Log("Enemy has been hit!");

                // this part is to animate the bullet

                int randomIndex = Random.Range(0, 3);
                animator.SetInteger("ExplosionType", randomIndex);
                animator.SetTrigger("Explode");

                bulletRB.linearVelocity = Vector2.zero;
                GetComponent<Collider2D>().enabled = false;

                Destroy(gameObject, 0.45f); // rmb to adjust this according to the animation length
            }

            if (firewallHealth != null)
            {
                firewallHealth.TakeDamage(damage);
                Debug.Log("Enemy has been hit!");

                // this part is to animate the bullet

                int randomIndex = Random.Range(0, 3);
                animator.SetInteger("ExplosionType", randomIndex);
                animator.SetTrigger("Explode");

                bulletRB.linearVelocity = Vector2.zero;
                GetComponent<Collider2D>().enabled = false;

                Destroy(gameObject, 0.45f); // rmb to adjust this according to the animation length
            }


            if (chatlogHealth != null)
            {
                chatlogHealth.TakeDamage(damage);
                Debug.Log("Enemy has been hit!");

                // this part is to animate the bullet

                int randomIndex = Random.Range(0, 3);
                animator.SetInteger("ExplosionType", randomIndex);
                animator.SetTrigger("Explode");

                bulletRB.linearVelocity = Vector2.zero;
                GetComponent<Collider2D>().enabled = false;

                Destroy(gameObject, 0.45f); // rmb to adjust this according to the animation length
            }
            **/
        }
    }
}