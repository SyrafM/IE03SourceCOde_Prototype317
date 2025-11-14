using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using Unity.VisualScripting;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    PlayableDirector director;
    [SerializeField]
    FirstPersonController player;
    [SerializeField]
    private float cutsceneTime;

    private void OnTriggerEnter(Collider other)
    {
        GameStateManagerScript.instance.setHud(true);
        director.Play();
        player.enabled = false;
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(cutsceneTime);
        player.enabled = true;
        this.setcoord();
        this.gameObject.SetActive(false);
    }

    public void setcoord()
    {
        GameStateManagerScript.instance.setcoords();
        GameStateManagerScript.instance.savedState = true;
    }
}
