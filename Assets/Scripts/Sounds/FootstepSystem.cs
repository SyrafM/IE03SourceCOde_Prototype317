using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;

public class FootstepSystem : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip marble;
    public AudioClip carpet;

    RaycastHit hit;
    public Transform rayStart;
    public float range;
    public LayerMask layerMask;

    public void Footstep()
    {
        if (Physics.Raycast(rayStart.position, rayStart.transform.up * -1, out hit, range, layerMask))
        {
            if (hit.collider.CompareTag("Marble"))
            {
                PlayFootstepSound(marble);
            }
            if (hit.collider.CompareTag("Carpet"))
            {
                PlayFootstepSound(carpet);
            }

        }
    }

    void PlayFootstepSound(AudioClip audio)
    {
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(audio);
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.DrawRay(rayStart.position, rayStart.transform.up * -1, Color.green);
    }
}
