using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public static StartMenuScript instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        GameStateManagerScript.instance.setstatemenu();
    }

    // Update is called once per frame
    void Update()
    {
        GameStateManagerScript.instance.setHud(false);
    }

    public void showcase()
    {
        GameStateManagerScript.instance.setHud(true);
        GameStateManagerScript.instance.setstate3D();
        GameStateManagerScript.instance.LoadScene3D("ShowcaseTutorial");
        
    }

    public void tutorial()
    {
        GameStateManagerScript.instance.setHud(true);

        GameStateManagerScript.instance.setstate3D();
        GameStateManagerScript.instance.LoadScene3D("Tutorial");
        
    }

    public void quitapp()
    {
        GameStateManagerScript.instance.QuitGame();
    }

}
