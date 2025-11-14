using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using Unity.VisualScripting;

public class DialogueTriggerGantry : MonoBehaviour
{
    public GameObject Player;
    public AudioSource audioSource;
    [SerializeField]
    AudioClip clip;
    public GameObject Office;

    private void OnTriggerEnter(Collider other)
    { 
        audioSource.PlayOneShot(clip);
        HudController.instance.EnableDialogueText("I think it requires a keycard to open. Maybe the receptionist has one...");
        HudController.instance.DelayedDisableTutorialText(8);
        this.gameObject.SetActive(false);
        Office.SetActive(true);
    }
    
}
