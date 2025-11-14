using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit2DTutorial : MonoBehaviour, IInteractable
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
            GameStateManagerScript.instance.setstate(true);
            GameStateManagerScript.instance.setinteract(true);
            GameStateManagerScript.instance.tutorialclear = true;
            GameStateManagerScript.instance.setstate3D();
            GameStateManagerScript.instance.setHud(true);
            
            SceneManager.LoadScene("Tutorial");
            
        }
    }
}
