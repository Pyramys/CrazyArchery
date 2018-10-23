using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField] private int int_score=0;
    [SerializeField] private int int_centerValue=5;
    [SerializeField] private int int_innerValue=3;
    [SerializeField] private int int_2ndOuterValue=2;
    [SerializeField] private int int_outerMostValue=1;

    [SerializeField] private Text txt_score;

    public int Int_score
    {
        get
        {
            return int_score;
        }

        set
        {
            int_score = value;
        }
    }

    public void AddScore(int section)
    {
        switch (section)
        {
            case 0:
                {
                    int_score += int_centerValue;
                    break;
                }
            case 1:
                {
                    int_score += int_innerValue;
                    break;
                }
            case 2:
                {
                    int_score += int_2ndOuterValue;
                    break;
                }
            case 3:
                {
                    int_score += int_outerMostValue;
                    break;
                }

            default:
                {
                    Debug.Log("Couldn't identify section 0 points awarded");
                    break;
                }
        }
        // Update Score Text.
        txt_score.text = "Score: " + int_score.ToString();
    }

    public void SubtractScore(int num, int obstacle)
    {
        int_score -= num;
    }

    public void SubtractScore(int num)
    {
        int_score -= num;
    }
    public void SetScore(int num)
    {
        int_score = num;
    }

    
}
