using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using Unity.VisualScripting;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject Player;
    public AudioSource audioSource;
    [SerializeField]
    AudioClip clip;

    private void OnTriggerEnter(Collider other)
    { 
        audioSource.PlayOneShot(clip);
        HudController.instance.EnableDialogueText("I must get home... I need to finish a task... But... where is home?");
        HudController.instance.DelayedDisableTutorialText(8);
        this.gameObject.SetActive(false);
    }
    
}
