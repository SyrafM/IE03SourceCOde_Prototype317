using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit2DBoss : MonoBehaviour, IInteractable
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
            SceneManager.LoadScene("Level2Video");
        }
    }
}
