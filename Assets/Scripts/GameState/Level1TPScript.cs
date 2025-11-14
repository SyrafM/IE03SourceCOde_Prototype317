using UnityEngine;

public class Level1TPScript : MonoBehaviour
{
    public static Level1TPScript instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        tpcheck();
    }
    public void tpcheck()
    {
        if (GameStateManagerScript.instance.savedState && GameStateManagerScript.instance.Exit2Dworld)
        {
            GameStateManagerScript.instance.tp();
            GameStateManagerScript.instance.Exit2Dworld = false;
        }
    }
}
