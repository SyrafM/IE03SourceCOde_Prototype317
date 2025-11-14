using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; // tracks closest interactable object
    public GameObject interactionIcon;
    private InputSystem_Actions _controls;

    private void Awake()
    {
        _controls = new InputSystem_Actions();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("E pressed, trying to interact");
            interactableInRange?.Interact();
        }
    }

    public void InteractButtonPressed()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        interactableInRange?.Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("Trigger Entered: " + collision.name);
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
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
