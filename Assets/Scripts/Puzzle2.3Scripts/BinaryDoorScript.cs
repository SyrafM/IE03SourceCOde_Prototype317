using UnityEngine;
using UnityEngine.SceneManagement;

public class BinaryDoorScript : MonoBehaviour, IInteractable
{
    public GameObject BinaryPuzzleBackground;
    public GameObject FirewallFolder;
    public GameObject DesktopBackground;
    public GameObject BinaryText;
    public GameObject BinaryDoor;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            BinaryPuzzleBackground.SetActive(false);
            BinaryText.SetActive(false);
            BinaryDoor.SetActive(false);

            FirewallFolder.SetActive(true);
            DesktopBackground.SetActive(true);
        }
    }
}
