using UnityEngine;

public class RWPCAccess : MonoBehaviour
{
    public GameObject gameo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //gameo.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
            gameo.gameObject.SetActive(true);
            Debug.Log(gameo.name + " has been activated.");
    }
}
