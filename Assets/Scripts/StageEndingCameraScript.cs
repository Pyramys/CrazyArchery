using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndingCameraScript : MonoBehaviour {

    // Script for handling what the camera does at the end of a level. Usually pans in and circles around the target.
    // To make it feel more natural, it will move to the current players camera, than move towards the target.
    [SerializeField] private bool isMoving;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float distanceToStop;
    [SerializeField] private float movingSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        // Move towards the target.
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, targetTransform.position) <= distanceToStop)
            {
                isMoving = false;
            }
        }
        else
        {
            Vector3.MoveTowards(transform.position, targetTransform.position, distanceToStop);
        }
    }

    private void OnEnable()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Target").transform;
        var camVector3 = GameObject.FindGameObjectWithTag("Main Camera").transform.position;
        transform.position = camVector3;
        isMoving = true;


    }
}
