using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : MonoBehaviour {
    //Peasants work like this, they pick a random location, move to it, stand around for a while, then move to the next point
    [SerializeField] private float flt_peasantDistance;
    [SerializeField] private float flt_idleTimer=3;
    [SerializeField] private float flt_defaultIdleTimer;
    [SerializeField] private float flt_idleTimeStamp;
    [SerializeField] private bool bool_isIdle;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float flt_speed;
    [SerializeField] private float flt_defaultSpeed;


    //MVP: Peasant script that just wanders around
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        flt_idleTimer = Random.Range(2, 5);
        flt_defaultIdleTimer = flt_idleTimer;
        flt_defaultSpeed = navMeshAgent.speed;
    }
    // Use this for initialization
    void Start () {
        navMeshAgent.SetDestination(RandomNavSphere());

    }
	
	// Update is called once per frame
	void Update () {
        if(!bool_isIdle)
        {
            MoveToPosition();
        }
        else
        {
            if(Time.time - flt_idleTimeStamp >= flt_idleTimer)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(RandomNavSphere());
                bool_isIdle = false;
            }
        }
        
	}    
    
    Vector3 RandomNavSphere()
    {
        NavMeshHit navHit;
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * flt_peasantDistance;

        randomDirection += transform.position;

        NavMesh.SamplePosition(randomDirection, out navHit, flt_peasantDistance, -1);

        return navHit.position;
    }

    void MoveToPosition()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            navMeshAgent.isStopped = true;
            bool_isIdle = true;
            flt_idleTimeStamp = Time.time;
        }
    }

    public void Enrage()
    {
        flt_idleTimer = .5f;
        navMeshAgent.speed = 8 * flt_defaultSpeed;
    }

    public void Unrage()
    {
        flt_idleTimer = flt_defaultIdleTimer;
        navMeshAgent.speed = flt_defaultSpeed;
    }

    public void End()
    {
        navMeshAgent.speed = 0;
    }

    //END MVP//---------------------------------


}
