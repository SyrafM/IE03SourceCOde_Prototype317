using UnityEngine;

public class TutorialSpawnCoord : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        this.setcoord();
    }

    public void setcoord()
    {
        GameStateManagerScript.instance.setcoords();
        GameStateManagerScript.instance.savedState = true;
    }
}
