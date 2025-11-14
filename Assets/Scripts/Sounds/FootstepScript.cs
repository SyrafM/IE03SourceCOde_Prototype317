using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class FootstepScript : MonoBehaviour
{
    private enum TerrainTags
    {
        Carpet,
        Marble
    }

    [SerializeField]
    private AudioClip[] footstepsAudios;

    private AudioSource audioSource;
    private float footstepTimer;
    public float timePerStep = 0.5f;
    private Rigidbody rb;
    public float movementThreshold = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()   
    {
        footstepTimer += Time.deltaTime;
        float currentSpeed = rb.linearVelocity.magnitude;
        if (currentSpeed > movementThreshold && audioSource.clip && footstepTimer > timePerStep)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSource.Play();
            footstepTimer = 0;
        }

        /*if (Input.GetKeyDown(KeyCode.LeftShift) && audioSource.clip && footstepTimer > timePerStep)
        {
            timePerStep = timePerStep/2;
            audioSource.Play();
            footstepTimer = 0;
        }*/
    }

    private void OnCollisionEnter(Collision col)
    {
        int index = 0;
        foreach(string tag in Enum.GetNames(typeof(TerrainTags)))
        {
            if(col.gameObject.tag == tag)
            {
                audioSource.clip = footstepsAudios[index];
            }
            index++;
        }
    }
}
