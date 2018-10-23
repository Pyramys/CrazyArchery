using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    [SerializeField] private float flt_timeHeld;
    [SerializeField] private Rigidbody rb;

    public float Flt_timeHeld
    {
        get
        {
            return flt_timeHeld;
        }

        set
        {
            flt_timeHeld = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision col)
    {        
        if(col.gameObject.tag == "Target")
        {
            transform.SetParent(col.gameObject.transform);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            switch (col.gameObject.name)
            {
                case "OuterMost":
                    {
                        break;
                    }
                case "2ndOuter":
                    {
                        break;
                    }
                case "Inner":
                    {
                        break;
                    }
                case "Center":
                    {
                        break;
                    }
                default:
                    {
                        Debug.Log("Did not recognize target portion");
                        break;
                    }
            }           
        }
        if(col.gameObject.tag == "Obstacle")
        {
            transform.SetParent(col.gameObject.transform);
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void FixedUpdate()
    {
        Constraints();
    }

    void Constraints()
    {
        if(transform.position.x > 100 || transform.position.y > 50 || transform.position.y < -30 || transform.position.z >10 || transform.position.z <-30)
        {
            Debug.Log(transform.position);
            Destroy(this.gameObject);
        }
    }
}
