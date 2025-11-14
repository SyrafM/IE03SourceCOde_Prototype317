using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToL2 : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameStateManagerScript.instance.setstate(true);
            GameStateManagerScript.instance.setinteract(true);
            GameStateManagerScript.instance.l1introclear = true;
            GameStateManagerScript.instance.Exit2Dworld = true;
            GameStateManagerScript.instance.setHud(true);
            GameStateManagerScript.instance.setcamera(false);
            SceneManager.LoadScene("Level 2 Showcase");

        }
    }
}
