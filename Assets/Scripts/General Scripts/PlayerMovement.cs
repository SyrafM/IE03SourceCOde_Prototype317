using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool running = false;
    Vector2 movementInput; // this stores both x and y inputs for our movement

    public float stamina;
    public float maxStamina;
    public float runCost;
    public Image StaminaBar;

    private Rigidbody2D rb;
    private InputSystem_Actions _controls;
    private Animator animator;
    private bool canSprint = true;

    private void Awake()
    {
        _controls = new InputSystem_Actions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stamina = maxStamina;
    }

    void Update()
    {
        float sprintInput = _controls.Player.Sprint.ReadValue<float>();

        if (!canSprint && sprintInput <= 0)
        {
            canSprint = true;                   // to allow player to sprint again after releasing the sprint key
        }

        if (sprintInput > 0 && stamina > 0 && canSprint)
        {
            running = true;

        }
        else
        {
            running = false;
        }

        if (running && (movementInput.x != 0 || movementInput.y != 0))
        {
            rb.linearVelocity = new Vector2(movementInput.x * moveSpeed * 2f, movementInput.y * moveSpeed * 2f); // this applies our inputs into our movement (i think)
            stamina -= runCost * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            if (stamina <= 0)
            {
                stamina = 0;
                running = false;
                canSprint = false;                 // prevent player from running until they release the button
            }

        }
        else
        {
            rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, movementInput.y * moveSpeed);
            if (sprintInput <= 0)
            {
                stamina += runCost * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0, maxStamina);
            }
        }

        StaminaBar.fillAmount = stamina / maxStamina;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", movementInput.x);
            animator.SetFloat("LastInputY", movementInput.y);
        }
        movementInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", movementInput.x);
        animator.SetFloat("InputY", movementInput.y);
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