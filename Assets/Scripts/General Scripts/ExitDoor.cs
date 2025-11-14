using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform teleportTarget; // where the player will be moved, can change in the inspector by putting an emmpty transform target

    public bool CanInteract()
    {
        return true; 
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;
            player.transform.rotation = teleportTarget.rotation;
        }
    }

}