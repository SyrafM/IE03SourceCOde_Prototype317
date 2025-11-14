using System.Collections;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public string location;
    public static TutorialScript instance;
    public GameObject jumpLocation;
    public GameObject wasdLocation;
    public GameObject wasdLock;
    public bool w;
    public bool a;
    public bool s;
    public bool d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStateManagerScript.instance.tutorialclear)
        {
            if (Input.GetKeyDown(KeyCode.Space) && location == "Space")
            {
                HudController.instance.EnableOneSubText("Press [Space] Complete");
                HudController.instance.EnableDialogueText("Jump Tutorial Completed");
                jumpLocation.SetActive(false);
                StartCoroutine("clear");
                location = "";
            }
            if (location == "wasd")
            {
                if (Input.GetKeyDown(KeyCode.W) && !w)
                {
                    HudController.instance.EnableMultipleSubText("Press [W] Completed", "NIL", "NIL", "NIL");
                    w = true;
                }
                if (Input.GetKeyDown(KeyCode.A) && !a)
                {
                    HudController.instance.EnableMultipleSubText("NIL", "Press [A] Completed", "NIL", "NIL");
                    a = true;
                }
                if (Input.GetKeyDown(KeyCode.S) && !s)
                {
                    HudController.instance.EnableMultipleSubText("NIL", "NIL", "Press [S] Completed", "NIL");
                    s = true;
                }
                if (Input.GetKeyDown(KeyCode.D) && !d)
                {
                    HudController.instance.EnableMultipleSubText("NIL", "NIL", "NIL", "Press [D] Completed");
                    d = true;
                }
                if (w == true && a == true && s == true && d == true)
                {

                    //HudController.instance.EnableOneSubText("Movement Tutorial Completed");
                    //HudController.instance.EnableDialogueText("Movement Tutorial Completed");
                    wasdLocation.SetActive(false);
                    //StartCoroutine("clear");
                    location = "";
                    wasdLock.SetActive(true);
                }
            }
        }
    }

    public void SetLocation(string location)
    {
        this.location = location;
    }
    
    IEnumerator clear()
    {
        yield return new WaitForSeconds(2);
        HudController.instance.DisableSubtextText();
        HudController.instance.DelayedDisableTutorialText(1);
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
    }
    public void setstate2D()
    { GameStateManagerScript.instance.setstate2D(); }
    
}
