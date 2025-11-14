using System.Collections;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public string introMessage;
    public string startObjectiveText;
    public string StartObjective;
    public GameObject cleanerbotdisabled;
    public GameObject cleanerbotenabled;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!GameStateManagerScript.instance.tutorialclear)
        {
            if (startObjectiveText != null) HudController.instance.EnableLeftTitleText(startObjectiveText);
        }
        if (GameStateManagerScript.instance.cleanerbot)
        {
            cleanerbotenabled.gameObject.SetActive(true);
            cleanerbotdisabled.gameObject.SetActive(false);
        }
        else
        {
            cleanerbotenabled.gameObject.SetActive(false);
            cleanerbotdisabled.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        this.setcoord();
        Debug.Log("Collider detected " + other.gameObject.tag);
        if (other.gameObject.tag == "Player" && !GameStateManagerScript.instance.l1introclear)
        {
            if (StartObjective != null) HudController.instance.EnableOneSubText(StartObjective);
            HudController.instance.EnableDialogueText(introMessage);
            HudController.instance.DelayedDisableTutorialText(5);
            this.gameObject.SetActive(false);
            GameStateManagerScript.instance.l1introclear = true;
        }
        
    }

    public void LoadScene(string scene)
    {
        GameStateManagerScript.instance.LoadScene3D(scene);
    }
    
    public void LoadScene2D(string scene)
    {
        GameStateManagerScript.instance.LoadScene2D(scene);
    }

    public void setHud(bool a)
    {
        GameStateManagerScript.instance.setHud(a);
    }

    public void setcoord()
    {
        GameStateManagerScript.instance.setcoords();
        GameStateManagerScript.instance.savedState = true;
    }
}
