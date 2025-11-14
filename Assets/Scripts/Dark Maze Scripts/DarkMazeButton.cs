using UnityEngine;
using UnityEngine.SceneManagement;

public class DarkMazeButton : MonoBehaviour, IInteractable
{
    public GameObject ButtonOff;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ButtonOff.SetActive(true);
            audioManager.PlaySFX(audioManager.taskComplete);
            GameStateManagerScript.instance.setstate(true);
            GameStateManagerScript.instance.setinteract(true);
            GameStateManagerScript.instance.l1introclear = true;
            GameStateManagerScript.instance.Exit2Dworld = true;
            GameStateManagerScript.instance.setHud(true);
            GameStateManagerScript.instance.setcamera(false);
            SceneManager.LoadScene("Level1");
        }
    }


}
