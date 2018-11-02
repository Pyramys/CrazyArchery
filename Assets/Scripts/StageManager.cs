using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StageManager : MonoBehaviour {
    // This script handles the stage.
    [SerializeField] private GameObject[] go_civilians;
    // Time for the event to start.
    [SerializeField] private float flt_eventStartTime=45;   
    [SerializeField] private float flt_eventTimeStamp;
    [SerializeField] private float flt_eventDuration = 15;
    [SerializeField] private bool bool_isEventHappening;
    [SerializeField] private Text txt_timerText;
    [SerializeField] private Player player;
    [SerializeField] private Target target;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject stageEndCamera;


    public float Flt_eventStartTime
    {
        get
        {
            return flt_eventStartTime;
        }

        set
        {
            flt_eventStartTime = value;
        }
    }

    private void Awake()
    {
        if(stageEndCamera == null)
        {
            stageEndCamera = GameObject.Find("StageEndingCamera");
        }        
    }
    // Use this for initialization.
    void Start ()
    {
        InitializeStage();

    }
	
	// Update is called once per frame.
	void Update ()
    {
		
	}

    private void InitializeStage()
    {
        go_civilians = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    void TrackEvent()
    {
        if(bool_isEventHappening)
        {
            if(Time.time - flt_eventTimeStamp >= flt_eventDuration)
            {
                EndEvent();
            }
        }
    }

    public void StartEvent()
    {
        txt_timerText.color = Color.red; 
        bool_isEventHappening = true;
        flt_eventTimeStamp = Time.time;
        foreach(var go in go_civilians)
        {
            go.GetComponent<Obstacle>().Enrage();
        }
        InvokeRepeating("TrackEvent", 1,1);
    }

    public void EndEvent()
    {
        txt_timerText.color = Color.black;
        foreach (var go in go_civilians)
        {
            go.GetComponent<Obstacle>().Unrage();
        }
        CancelInvoke("TrackEvent");
        bool_isEventHappening = false;
    }

    public void EndStage()
    {
        // Stop the scene.        
        target.End();       
        foreach (var go in go_civilians)
        {
            go.GetComponent<Obstacle>().End();
        }
        // Move Camera to Target, linger until player taps screen.
        player.ToggleCamera(false);
        // Activate the Stage End camera, and have it zoom in
        stageEndCamera.SetActive(true);

        

        //player.MoveToTarget();
        // Disable the scene.
        // Trigger Ad.
        // Bring up score Screen.
        // Change stars color based on performance.
    }
}
