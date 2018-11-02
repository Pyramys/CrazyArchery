using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Script that controls all player actions.
    // mainly reading input from the user and tracking certain things on the player.
    // The biggest thing it does is fire arrows from the bow area.

    [SerializeField] private Vector3 vec3_arrowDefaultPosition;

    // Arrow firing logic.
    // Player taps screen to fire an arrow.
    // Player holds tap to draw arrow back.
    // Player releases tap to launch an arrow, and a script on the arrow handles its actions and itneractions.
    
    [SerializeField] private bool bool_isHolding = false;
    [SerializeField] private float flt_timeHolding;
    // Reference to what the bow will be firing.
    [SerializeField] private GameObject go_arrow;
    // Reference to the exact arrow loaded unto bow.
    [SerializeField] private GameObject go_loadedArrow;         
    [SerializeField] private Vector3 vec3_originalRotation;
    [SerializeField] private bool bool_isReloading;
    // Time Spent Reloading.
    [SerializeField] private float flt_reloadTime=.5f;
    // Record the time player started reloading.
    [SerializeField] private float flt_startReloadTimeStamp;        
    [SerializeField] private float flt_arrowVelocityRampUp;
    [SerializeField] private bool bool_isEnded;
    [SerializeField] private GameObject go_target;
    [SerializeField] private Camera playerCamera;


    private void Start()
    {

        vec3_arrowDefaultPosition = go_loadedArrow.transform.localPosition;
        vec3_originalRotation = go_loadedArrow.transform.eulerAngles;
        
    }

    private void Update()
    {
        if (bool_isHolding)
        {
            flt_timeHolding += flt_arrowVelocityRampUp * Time.deltaTime;
            if(flt_timeHolding >= 25)
            {
                flt_timeHolding = 25;
            }
        }
        if(bool_isReloading)
        {
            if(Time.time - flt_startReloadTimeStamp >= flt_reloadTime )
            {
                Reload();
            }
        }
        GetInput();
        if(bool_isEnded)
        {
            MoveToTarget();
        }        
    }

    void GetInput()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!bool_isReloading)
            {
                FireBow();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            bool_isHolding = true;
            // Begins holding the bow, this is how the player can move the camera around and aim his bow.
        }
        // DEBUG ONLY.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
        // DEBUG MODE BUTTON.
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject.Find("StageManager").GetComponent<StageManager>().EndStage();
        }
    }

    void FireBow()
    {
        // Fire the bow straight ahead .
        go_loadedArrow.transform.parent = null;
        go_loadedArrow.GetComponent<Rigidbody>().isKinematic = false;
        go_loadedArrow.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * flt_timeHolding*5;
        go_loadedArrow.GetComponent<Arrow>().Flt_timeHeld = flt_timeHolding;

        // Begin reload process.
        flt_startReloadTimeStamp = Time.time;
        
        // Reset variables.
        bool_isHolding = false;
        flt_timeHolding = 0;
        bool_isReloading = true;
    }
    
    void Reload()
    {
        bool_isReloading = false;
        go_loadedArrow = Instantiate(go_arrow, vec3_arrowDefaultPosition, Quaternion.identity) as GameObject;
        go_loadedArrow.transform.SetParent(this.gameObject.transform);
        go_loadedArrow.transform.localPosition = vec3_arrowDefaultPosition;
        go_loadedArrow.transform.localRotation = Quaternion.Euler(vec3_originalRotation);
    }

    public void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, go_target.transform.position) < 3)
        {
            return;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, go_target.transform.position, 2);
        }   
    }

    public void ToggleCamera(bool val)
    {
        playerCamera.enabled = val;
    }
}
