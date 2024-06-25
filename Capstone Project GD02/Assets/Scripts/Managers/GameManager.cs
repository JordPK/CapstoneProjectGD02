using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentDay;

    public static GameManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetTimescale(float Speed)
    {
        Time.timeScale = Speed;
    }

    public void CheckForCaptain()
    {
        GameObject captain = GameObject.FindAnyObjectByType<FPSPlayerMovement>().gameObject;
        if(captain == null)
        {
            Debug.Log("GAME OVER");
            GameOver();
            //insert reference to a function in the UI manager that will handle the game over screen.
        }
    }

    public void GameOver()
    {
        ScoreEvaluation.Instance.EvaluateScore(EventManager.Instance.GetPercentage());
        Debug.Log("Grade: " + ScoreEvaluation.Instance.grade); //move this to UI Manager for actual end screen updates
    }

    public void GameVictory()
    {
        ScoreEvaluation.Instance.EvaluateScore(EventManager.Instance.GetPercentage());
        Debug.Log("Grade: " + ScoreEvaluation.Instance.grade); //move this to UI Manager for actual end screen updates
    }
}
