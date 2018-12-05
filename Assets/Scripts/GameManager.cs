using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //Script for managing the state of the game and overall app settings
    [SerializeField] private int int_currentLevel=0;

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public int Int_currentLevel
    {
        get
        {
            return int_currentLevel;
        }

        set
        {
            int_currentLevel = value;
        }
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


    public void LoadLevel(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void SetLevel(int num)
    {
        int_currentLevel = num;
    }

}
