using UnityEngine;
using UnityEngine.SceneManagement;

public class FirewallFolderScript : MonoBehaviour, IInteractable
{
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SceneManager.LoadScene("2D Firewall Boss");
        }
    }    
}
