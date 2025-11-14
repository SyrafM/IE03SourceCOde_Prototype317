using UnityEngine;

public class TpOnLoadScript : MonoBehaviour
{
    public static TpOnLoadScript instance;
    public GameObject lift1;
    public GameObject lift2;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject Cube1;
    public GameObject door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManagerScript.instance.tutorialclear) { tpcheck(); disablePlane(); }
        liftcheck();
    }
    public void disablePlane()
    {
        plane1.gameObject.SetActive(false);
        plane2.gameObject.SetActive(false);
        Cube1.gameObject.SetActive(true);
    }


    public void tpcheck()
    {
        if (GameStateManagerScript.instance.savedState)
        {
            GameStateManagerScript.instance.tp();
        }
    }
    public void liftcheck()
    {
        if (GameStateManagerScript.instance.tutorialclear)
        {
            lift1.gameObject.SetActive(false);
            lift2.gameObject.SetActive(true);
        }
    }

    
}
