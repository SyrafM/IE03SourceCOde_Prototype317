using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowcaseExit : MonoBehaviour, IInteractable
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
            GameStateManagerScript.instance.cleanerbot = true;
            GameStateManagerScript.instance.setstate3D();
            GameStateManagerScript.instance.Exit2Dworld = true;
            GameStateManagerScript.instance.setHud(true);

            SceneManager.LoadScene("Level 2 Bot Off");

        }
    }
}
