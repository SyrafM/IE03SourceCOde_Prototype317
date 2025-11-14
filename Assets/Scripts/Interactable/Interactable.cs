using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string keytopress;
    public string raycastMessage; //used by PlayerInteraction to display message when looked at
    public string successfulInteractMessage;
    public string delayedSuccessfulInteractMessage;
    public string requiredMessage;
    public string usedMessage;
    public string objectiveMessage;
    public string updateObjectiveTitleMessage;
    public int delayseconds;
    public bool disableInventoryItem;
    public bool updateObjectives;
    public bool updateObjectiveTitle;
    public bool requireSuccessfulInteractMsg;
    
    
    


    public UnityEvent onInteraction;
    public GameObject InventoryItem;
    public GameObject RequiredItem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (InventoryItem != null && disableInventoryItem == true) //Disable specified inventory item on generation
        {
            InventoryItem.SetActive(false);
        }

        outline = GetComponent<Outline>();
        outline.enabled = false; //disable outline on generation
    }
    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }

    public void Interact()
    {

        if (updateObjectiveTitle == true)
        {
            HudController.instance.EnableLeftTitleText(updateObjectiveTitleMessage);
        }
        if (RequiredItem == null)
        {

            onInteraction.Invoke();
            


                if (successfulInteractMessage != null)
                {
                    HudController.instance.EnableDialogueText(successfulInteractMessage);
                    HudController.instance.DelayedDisableTutorialText(delayseconds);

                }
                if (updateObjectives)
                {
                    HudController.instance.EnableOneSubText(objectiveMessage);
                }
            }
            else if (RequiredItem.activeSelf == true)
            {
                onInteraction.Invoke();

                if (requireSuccessfulInteractMsg)
                {
                    HudController.instance.EnableDialogueText(usedMessage);
                    HudController.instance.DelayedDisableTutorialText(delayseconds);

                }
            }
            else
            {
                HudController.instance.EnableDialogueText(requiredMessage);
                HudController.instance.DelayedDisableTutorialText(delayseconds);
            }
        }
    
    

    public void disableInteract(GameObject gameObject)
    {
        gameObject.tag = "Untagged";
    }
    public void enableInteract(GameObject gameObject)
    {
        gameObject.tag = "Interactable";
    }
   

}
