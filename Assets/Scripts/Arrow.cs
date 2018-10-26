using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    [SerializeField] private float flt_timeHeld;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isStuck;

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
        if (!isStuck)
        {
            if (col.gameObject.tag == "Target")
            {
                // Stop the arrow, remove further physics on it, set its transform to the target so it follows it, and freeze all movement on axes.                
                rb.velocity = new Vector3(0, 0, 0);
                rb.isKinematic = true;
                transform.SetParent(col.gameObject.transform);
                rb.constraints = RigidbodyConstraints.FreezeAll;
                // Scoring is handled by the individual sections of the target. Keeping this here just in case I want the arrow to do something unique when hitting any portion.
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
                isStuck = true;
            }
            if (col.gameObject.tag == "Obstacle")
            {
                // Handle when obstcale is hit.
                // Default Debug.log until need to handle this.
                Debug.Log("Hit Obstacle");

                // Moving this out of scope for the MVP, the players ability to see arrows sticking out of non-targets is irrelavant.
                // Also not removing arrows effects or destroying arrows on obstacles because they can be quite, hilarious.
                /*rb.velocity = new Vector3(0, 0, 0);
                rb.isKinematic = true;
                transform.SetParent(col.gameObject.transform);
                isStuck = true;*/
            }
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
