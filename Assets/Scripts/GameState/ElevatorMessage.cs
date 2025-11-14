using UnityEngine;
using TMPro;
using System.Collections;

public class ElevatorMessage : MonoBehaviour
{
    public string firstString;
    public string secondString;
    public string thirdString;
    public void interact()
    {
        StartCoroutine("One");
    }

    private IEnumerator One()
    {
        yield return new WaitForSeconds(2);
        HudController.instance.EnableDialogueText(firstString);
        StartCoroutine("Two");
    }

    private IEnumerator Two()
    {
        yield return new WaitForSeconds(2);
        HudController.instance.EnableDialogueText(firstString);
        StartCoroutine("Three");
    }
    private IEnumerator Three()
    {
        yield return new WaitForSeconds(2);
        HudController.instance.EnableDialogueText(firstString);
        yield return new WaitForSeconds(2);
        HudController.instance.DisableDialogueText();
    }
        
}
