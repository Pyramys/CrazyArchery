using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    //This Script controls the target as a whole
    //Most has to do with its movement
    [SerializeField] private Transform trans;

    //FOR MVP: Need it to just move left and right not too fast
    [SerializeField] private float flt_moveSpeed=1;
    [SerializeField] private float flt_leftConstraint = -5f;
    [SerializeField] private float flt_rightConstraint = 5;
    [SerializeField] private Vector3 vec3_direction;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        vec3_direction = new Vector3(1, 0, 0);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(trans.position.x >= flt_rightConstraint)
        {
            vec3_direction = new Vector3(-1, 0, 0);
        }
        if(trans.position.x <= flt_leftConstraint)
        {
            vec3_direction = new Vector3(1, 0, 0);
        }
        trans.position += vec3_direction * flt_moveSpeed * Time.deltaTime;
	}

    public void End()
    {
        flt_moveSpeed = 0;
    }
}
