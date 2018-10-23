using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    // This script manages the UI for the game.
    // It will require certain UI states that will enable and disable UI Panels.
    // Reference to various objects and scripts.
    // 0 is Main Menu, 1 is Level Select,  2 is Pause,  3 is Scene,   4 is End Scene.
    [SerializeField] private int int_UIState = 0; 

    [SerializeField] private GameObject go_MainMenuPanel;
    [SerializeField] private GameObject go_LevelSelectPanel;
    [SerializeField] private GameObject go_PausePanel;
    [SerializeField] private GameObject go_ScenePanel;
    [SerializeField] private GameObject go_EndScenePanel;

    // Time variables and Components.
    // Running second Timer.
    [SerializeField] private float flt_secondTime=60;
    // Running Minute Timer.
    [SerializeField] private float flt_minuteTime=2;
    // UI version of the time.
    [SerializeField] private Text txt_time;         
    [SerializeField] private Slider slider_timeSlider;
    [SerializeField] private float flt_percentage;

    // Other Components.
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StageManager stageManager;
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get { return _instance; }
    }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization.
    void Start () {
        int_UIState = 3;
        slider_timeSlider.value = 1;
        UpdateUI();
	}
	
	// Update is called once per frame.
	void Update () {
        
	}

    void UpdateUI()
    {
        DisableAllPanels();
        switch (int_UIState)
        {
            // Main Menu.
            case 0: 
                {
                    go_MainMenuPanel.SetActive(true);
                    break;
                }
            // Level Select.
            case 1: 
                {
                    go_LevelSelectPanel.SetActive(true);
                    break;
                }
            // Pause.
            case 2:
                {
                    go_PausePanel.SetActive(true);
                    break;
                }
            // Scene.
            case 3: 
                {
                    go_ScenePanel.SetActive(true);
                    StartTimer();
                    break;
                }
            // End Scene.
            case 4: 
                {
                    go_EndScenePanel.SetActive(true);
                    break;
                }
            default: 
                {
                    Debug.Log("Cant recognize UI State");
                    break;
                }
        }
    }

    void DisableAllPanels()
    {
        // Disable all UI Panels to enable only the current one.
        go_EndScenePanel.SetActive(false);
        go_MainMenuPanel.SetActive(false);
        go_LevelSelectPanel.SetActive(false);
        go_PausePanel.SetActive(false);
        go_ScenePanel.SetActive(false);
    }

    public void ChangeUIState(int num)
    {
        int_UIState = num;
        UpdateUI();
    }

    public void StartTimer()
    {
        InvokeRepeating("Timer", 1, 1);
    }

    void Timer()
    {
        // Slowly decrements second timer from 60, once it reaches 0 decrement a minute.
        flt_secondTime -= 1;
        if (flt_secondTime == 60 - stageManager.Flt_eventStartTime)
        {
            Debug.Log("START EVENT");
            stageManager.StartEvent();
        }   
        
        flt_percentage = (1f / 120f);
        slider_timeSlider.value -= flt_percentage;
        if(flt_secondTime <= 0)
        {
            flt_secondTime = 60;
            flt_minuteTime -= 1;
            if(flt_minuteTime < 0)
            {
                // End Match.
                flt_secondTime = 60;
                flt_minuteTime = 2;
                stageManager.EndStage();
                ChangeUIState(4);
                slider_timeSlider.value = 1;
            }
        }
        // Translates the value into UI text.
        if(flt_secondTime == 60)
        {
            txt_time.text = ((flt_minuteTime+1).ToString() + " : 00");
        }
        else if (flt_secondTime >=10)
        {
            txt_time.text = (flt_minuteTime.ToString() + " : " + Mathf.FloorToInt(flt_secondTime).ToString());
        }
        else
        {
            txt_time.text = (flt_minuteTime.ToString() + " : 0" + Mathf.FloorToInt(flt_secondTime).ToString());
        }
        
    }

    public void LoadLevel(int num)
    {
        gameManager.LoadLevel(num);
    }
}
