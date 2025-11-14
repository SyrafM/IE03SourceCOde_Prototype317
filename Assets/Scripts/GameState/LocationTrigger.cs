using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public string Lmessage;
    public string Lmessage2;
    public string Lmessage3;
    public string Lmessage4;
    public string Lmessage5;
    public string LLocation;
    public bool multipleSubtext;
    private void OnTriggerEnter(Collider other)
    {
        if (!GameStateManagerScript.instance.tutorialclear)
        {
            Debug.Log("Collider detected " + other.gameObject.tag);
            if (other.gameObject.tag == "Player")
            {

                if (multipleSubtext)
                {
                    HudController.instance.EnableDialogueText(Lmessage5);
                    HudController.instance.EnableMultipleSubText(Lmessage, Lmessage2, Lmessage3, Lmessage4);
                    StopAllCoroutines();
                    StartCoroutine("test", 5);
                }
                else
                {
                    HudController.instance.EnableDialogueText(Lmessage);
                    HudController.instance.EnableOneSubText(Lmessage);
                    StopAllCoroutines();
                    StartCoroutine("test", 2);
                }


                TutorialScript.instance.SetLocation(LLocation);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HudController.instance.DisableDialogueText();
        }
    }

    IEnumerator test(int a)
    {
        yield return new WaitForSeconds(a);

        HudController.instance.DisableDialogueText();

    }
}
