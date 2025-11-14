using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
    public GameObject holePrefab;
    public Vector2 spawnMin;
    public Vector2 spawnMax;
    public float breakTime;

    public Sprite leftHole;
    public Sprite rightHole;

    public Animator firewallAnimator;

    private float timer;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > breakTime)
        {
            timer = 0f;
            spawnHole();
        }
    }

    public void spawnHole()
    {
        Vector2 randomPos = new Vector2(Random.Range(spawnMin.x, spawnMax.x), Random.Range(spawnMin.y, spawnMax.y));

        GameObject newHole = Instantiate(holePrefab, randomPos, Quaternion.identity);
        audioManager.PlaySFX(audioManager.bossAttackTwo);

        SpriteRenderer sr = newHole.GetComponent<SpriteRenderer>();

        if (randomPos.x < (spawnMax.x + spawnMin.x)/2)
        {
            sr.sprite = leftHole;
            firewallAnimator.SetTrigger("SpawnLeftHole");
        }
        else if (randomPos.x > (spawnMax.x + spawnMin.x) / 2)
        {
            sr.sprite = rightHole;
            firewallAnimator.SetTrigger("SpawnRightHole");
        }
    }
}
