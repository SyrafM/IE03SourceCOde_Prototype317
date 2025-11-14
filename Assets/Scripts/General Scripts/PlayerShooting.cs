using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject playerBullet;
    public Transform bulletPos;
    public bool shooting = false;

    public int maxCharges;
    public int currentCharges;
    public float chargeCooldown;
    public float[] chargeTimers;
    public Image chargeBarImage;
    public Sprite[] chargeSprites;

    private InputSystem_Actions _controls;
    private float timer;

    AudioManager audioManager;

    private void Awake()
    {
        _controls = new InputSystem_Actions();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentCharges = maxCharges;
        chargeTimers = new float[maxCharges];
        for (int i = 0; i < maxCharges; i++)
        {
            chargeTimers[i] = 0f;
        } 
            
    }
    // Update is called once per frame
    void Update()
    {
        if (_controls.Player.Shooting.ReadValue<float>() > 0)
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }

        // recharge if not at full charge
        for (int i = 0; i < maxCharges; i++)
        {
            if (currentCharges < maxCharges &&  chargeTimers[i] > 0f)
            {
                chargeTimers[i] -= Time.deltaTime;          // figure out that the chargecooldown is active for that timer so it counts down to 0.
                if (chargeTimers[i] <= 0f)                  // now the cooldown is over so reset 
                {
                    chargeTimers[i] = 0f;
                    if (currentCharges < maxCharges)
                    {
                        currentCharges++;
                        chargeBarImage.sprite = chargeSprites[currentCharges];
                    }
                }
            }
        }

        // attempt shooting
        if (shooting && currentCharges > 0)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                timer = 0f;
                shoot();
                consumeCharge();
            }
        }

    }

    void consumeCharge()
    {
        if (currentCharges > 0)
        {
            for (int i = 0; i < maxCharges; i++)
            {
                if (chargeTimers[i] == 0f)
                {
                    chargeTimers[i] = chargeCooldown;
                    currentCharges--;
                    chargeBarImage.sprite = chargeSprites[currentCharges];
                    break;
                }
            }
        }
    }

    void shoot()
    {
        audioManager.PlaySFX(audioManager.playerShoot);
        Instantiate(playerBullet, bulletPos.position, Quaternion.identity);
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}