using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentDay = 1;

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
            GameOver();
            //insert reference to a function in the UI manager that will handle the game over screen.
        }
    }

    public void GameOver()
    {
        ScoreEvaluation.Instance.EvaluateScore(EventManager.Instance.GetPercentage());
        UIManager.Instance.GameOverScreen();
    }

    public void GameVictory()
    {
        ScoreEvaluation.Instance.EvaluateScore(EventManager.Instance.GetPercentage());
        UIManager.Instance.VictoryScreen();
    }
}
