using System.Collections;
using Unity.AppUI.Core;
using UnityEngine;

public class CatBehaviour : MonoBehaviour
{
    public Transform player;
    public float runSpeed;              // speed at which the cat runs at the player
    public float runTime;               // amount of time that the cat can run at the player
    public float roamSpeed;             // speed at which the cat roams around the player area
    public float roamTime;              // amount of time that the cat will roam, TO BE CHANGED TO A RANGE
    public float chargeTime;            // after roam time is up, the cat will stop momentarily and telegraph that it's gonna run.
    public float cooldownTime;          // idk i might make it so that the cat will stop for a moment?????? ask the team
    public Transform center;
    public float stageTime;
    public float overallTimer;
    public GameObject ClawSpawner;
    public GameObject Cat;
    public GameObject CatBed;
    public Transform inactivePos;
    public Transform centerPos;
    public float waitTime;
    

    public bool canRun;
    public bool isCharging;
    
    public int damage;

    private Rigidbody2D rb;
    private float timer;
    private Vector2 roamTarget;         // this is used for the cat to pick random spots in the arena to pathfind to and basically roam
    private bool hasRoamTarget;
    private SpriteRenderer catSprite;
    private Color originalColor;
    private Collider2D col;
    private bool isActive = false;
    private Animator animator;
    private Vector2 catPos;

    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        catSprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        originalColor = catSprite.color;
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnEnable()
    {
       transform.position = inactivePos.position;
       StartCoroutine(EnterSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        timer += Time.deltaTime;
        overallTimer += Time.deltaTime;
        // player = GameObject.FindGameObjectWithTag("Player");

        if (timer < roamTime)
        {
            // the cat will just roam;
            Roam();
        } 
        else if (timer > roamTime && timer < roamTime +chargeTime)
        {
            // if the timer is more than roam time, then it's time to change into charging mode
            isCharging = true;
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);

            // while it's charging, the cat will turn translucent 
            if (catSprite != null)
            {
                catSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f);
            }
        }
        else if (timer > roamTime + chargeTime)
        {
            // charge time is up! now the cat will run at the player
            isCharging = false;
            canRun = true;
            // turn fully visible/red/solid when running at the player
            if (catSprite != null)
            {
                catSprite.color = new Color(1f, 0.8f, 0.8f, 1f);
            }
            Run();
        }

        if (canRun && timer > roamTime + chargeTime + runTime)
        {
            // run time is up! now the cat will stop running and it's gonna reset and go back to roaming.
            timer = 0f;
            canRun = false;
            isCharging = false;
            catSprite.color = originalColor;
        }
        // make it so that the cat can only move vertically or horizontally when running
        // give it time to jump up and down for retreating and entering arena 
        if (overallTimer > stageTime)
        {
            catSprite.color = originalColor;
            overallTimer = 0f;
            StartCoroutine(RetreatSequence());
        }
        
    }

    void Roam()
    {
        if (!hasRoamTarget)
        {
            // if the cat hasn't been assigned a roam target yet, it will choose a random point within a circle around the player.
            // Vector2 randomOffset = Random.insideUnitCircle * 7f;                // gives a random point inside a circle with coords (x, y). change the number 5f if you want it to choose from a bigger circle
            // roamTarget = (Vector2)player.position + randomOffset;               // in this line, you take the player's position as the centre of the circle, then the roam target is a random offset within the circle
            // roamTarget = (Vector2)center.position + randomOffset;               // im keeping these here just in case...

            // if the cat hasn't been assigned a roam target yet, it will choose a random point within the bounds set in the random range
            // note that when putting in the numbers here, the numbers have to be LESS than the actual bounds of the walls 
            // the cat tends to glitch out and not change target if the roam target is too near the wall
            float randomX = Random.Range(-16, 16);
            float randomY = Random.Range(-6, 2);                        
            roamTarget = new Vector2(randomX, randomY);
            
            hasRoamTarget = true;
            animator.SetBool("isWalking", true);

            Vector2 dir = (roamTarget - (Vector2)transform.position).normalized * roamSpeed;

            animator.SetFloat("InputX", dir.x);
            animator.SetFloat("InputY", dir.y);
        }

        Vector2 moveDir = (roamTarget - (Vector2)transform.position).normalized * roamSpeed;
        rb.linearVelocity = moveDir;

        // once the cat gets close to the target, it'll tell the cat to pick a new one target so it can move to a different place.
        if (Vector2.Distance(transform.position, roamTarget) < 0.5f)
        {
            hasRoamTarget = false;
            // let it wait first
            //StartCoroutine(WaitForRoam());

        }

    }

    void Run()
    {
        if (player != null)
        {
            Vector2 moveDir = (player.transform.position - transform.position).normalized * runSpeed;
            rb.linearVelocity = moveDir;
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", moveDir.x);
            animator.SetFloat("InputY", moveDir.y);

        }
    }

    // this part is for debugging - it shows the area that the cat can roam in ig
    void OnDrawGizmos()
    {
        if (player != null)
        {
            // nvm idk how to draw a BOX.
            Gizmos.color = Color.yellow;
            // Gizmos.DrawWireCube(center.position, );

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
    private IEnumerator WaitForRoam()
    {
        Debug.Log("yeah its waiting");
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(waitTime);
    }
    
    private IEnumerator RetreatSequence()
    {
        rb.linearVelocity = Vector2.zero;
        col.enabled = false;
        yield return new WaitForSeconds(1.0f);

        while (Vector2.Distance(transform.position, inactivePos.position) > 0.1f)
        {
            Vector2 moveDir = ((Vector2)inactivePos.position - (Vector2)transform.position).normalized * roamSpeed;
            rb.linearVelocity = moveDir;
            yield return null;
        }
        rb.linearVelocity = Vector2.zero;
        // this is the animation segment?
        // animator.SetTrigger("JumpOut");
        // yield return new WaitForSeconds(jumpAnimDuration);
        ClawSpawner.SetActive(true);
        Cat.SetActive(false);
        CatBed.SetActive(true);
        // yield return new WaitForSeconds(2.0f);
        yield return null;                      // this is how it is for now uhhhhhhh while thinking of how to do the animations
    }

    private IEnumerator EnterSequence()
    {
        col.enabled = false;
        yield return new WaitForSeconds(2.0f);
        while (Vector2.Distance(transform.position, centerPos.position) > 0.1f)
        {
            Vector2 moveDir = ((Vector2)centerPos.position - (Vector2)transform.position).normalized * roamSpeed;
            rb.linearVelocity = moveDir;
            // Debug.Log("Moving to center: " + transform.position);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        rb.linearVelocity = Vector2.zero;
        col.enabled = true;
        isActive = true;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                // audioManager.PlaySFX(audioManager.bossAttackOne);
            }
        }
    }
    /**
     * 
    This is the logic segment so i don't forget what i did here:
    the idea is that the cat will roam around the battlefield for a time (roamTime) at a given roamSpeed. for now, it's fixed.
    i can try randomizing this within a RandomRange or smth later on.
    it will keep roaming until the roamtime is up, after which it will charge up. during this time, the cat should give a small window to
    charge up and telegraph to the player that it will charge at it.
    then, it will run in a straight line towards the player at a given runSpeed for a given runTime. then, it will cooldown (Not implemented yet), and then return
    to its roaming stage again. 
    logic with the scratch attack:
    idk i might make it so that this whole segment can only last a set amount of time, like maybe 15 seconds, then it will tell the cat to retreat to a transform point out of the player's bounds
    then the scratching will commence
    ty for reading
     * 
     */
}