using UnityEngine;

public class L1ElevatorButtons : MonoBehaviour
{

    public GameObject elevator1;
    public GameObject elevator2;
    // Update is called once per frame
    void Update()
    {
        if(GameStateManagerScript.instance.L1CatBoss)
        {
            elevator1.SetActive(true);
            elevator2.SetActive(false);
            
        }
    }
}
