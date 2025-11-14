using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using Unity.VisualScripting;

public class CutsceneManager : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    PlayableDirector director;
    [SerializeField]
    FirstPersonController player;
    [SerializeField]
    private float cutsceneTime;
    public GameObject Plane;

    private void OnTriggerEnter(Collider other)
    {
        director.Play();
        player.enabled = false;
        StartCoroutine(FinishCut());
       
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(cutsceneTime);
        player.enabled = true;

        if (Plane)
        { Plane.gameObject.SetActive(true); }
        GameStateManagerScript.instance.ResumeGame();
        this.setcoord();
    }

    public void setcoord()
    {
        GameStateManagerScript.instance.setcoords();
        GameStateManagerScript.instance.savedState = true;
    }
}

