using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CleanerBotAnimationPlane : MonoBehaviour
{
    public PlayableDirector Director;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameStateManagerScript.instance.cleanerbot == true && GameStateManagerScript.instance.cleanerbotcutscene==false)
        {
            GameStateManagerScript.instance.cleanerbotcutscene = true;
            Director.Play();
        }

    }
}
