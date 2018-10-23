using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSection : MonoBehaviour {

    // 0 is center, 1 is inner, 2 is 2nd outer, 3 is outermost.
    [SerializeField] private int int_section; 
    [SerializeField] private ScoreManager scoreManager;



    private void OnCollisionEnter(Collision col)
    {
        scoreManager.AddScore(int_section);
    }

}
