using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEvaluation : MonoBehaviour
{
    public string grade;


    public static ScoreEvaluation Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        
        //ScoreEvaluation.Instance.EvaluateScore(EventManager.Instance.GetPercentage());
    }

    public void EvaluateScore(float shipRemaining)
    {
        
        switch (shipRemaining)
        {
            case >= 100:
                grade = "SS";
                break;
            case >= 95:
                grade = "S";
                break;
            case >= 90:
                grade = "A*";
                break;
            case >= 80:
                grade = "A";
                break;
            case >= 70:
                grade = "B";
                break;
            case >= 60:
                grade = "C";
                break;
            case >= 50:
                grade = "D";
                break;
            case >= 40:
                grade = "E";
                break;
            default:
                grade = "F";
                break;
        }
        
    }
}

