using UnityEngine;

public class PathButtonScript : MonoBehaviour, IInteractable
{
    public GameObject buttonOff;
    public GameObject buttonOn;
    public GameObject PathOne;
    public GameObject PathTwo;

    public bool isPathOne = true;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            buttonOff.SetActive(!buttonOff.activeSelf);
            buttonOn.SetActive(!buttonOn.activeSelf);
            
            if (buttonOn.activeSelf)
            {
                isPathOne = true;
                PathOne.SetActive(true);
                PathTwo.SetActive(false);

            }
            else
            {
                isPathOne= false;
                PathOne.SetActive(false);
                PathTwo.SetActive(true);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
