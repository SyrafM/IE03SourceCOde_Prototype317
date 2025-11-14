using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.AI;

public class PatrolBot : MonoBehaviour
{

    [SerializeField]
    private Transform[] points;

    private int destinationPoint = 0;
    private NavMeshAgent Agent;

    [SerializeField]
    private float minRemainingDistance = 0.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent. autoBraking = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(!Agent.pathPending && Agent.remainingDistance < minRemainingDistance) //to check whether it reached the final destiantion or not
        {
            GoToNextPoint();
        }
    }
    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            Debug.LogError("You must setup at least one destination point");
            enabled = false;
            return;
        }

        Agent.destination = points[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % points.Length;
    }
}
