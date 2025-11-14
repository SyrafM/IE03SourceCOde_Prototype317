using UnityEngine;

public class PressButton : MonoBehaviour
{
    public bool ChangePoints = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isPressed(bool pressed)
    {
        if (pressed)
        {
            ChangePoints = false;
        }
        else
        {
            ChangePoints = true;
        }
    }
}
