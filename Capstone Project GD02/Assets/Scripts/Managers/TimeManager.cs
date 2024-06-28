using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public TextMeshProUGUI timerText;
    public float currentTime;
    public int dayDuration;

    public int dailyEject;

    public string dailyEvent;

    // Start is called before the first frame update
    void Start()
    {
        NewWeightGoal();
    }

    // Update is called once per frame
    void Update()
    {
        //DisplayTimer();
        DayCycle();

    }

    public void DayCycle()
    {
        currentTime += Time.deltaTime;
       
        // if currentTime hits dayDuration add increase the day by 1
        if (currentTime >= dayDuration)
        {
            currentTime = 0;
            GameManager.Instance.currentDay++;

            //checks to see if day 10 was just finished and ends the game if it was
            if(GameManager.Instance.currentDay == 11)
            {
                GameManager.Instance.GameVictory();
                //insert a reference to the UI manager that handles the victory screen
            }


            DailyCrewCharge();
            NewWeightGoal();
            GenerateRandomEvent();

            
            //Debug.Log(GameManager.Instance.currentDay);
        }
    }

    /*void DisplayTimer()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60.0f); // Dividing by 60 (number of seconds then going down to the lowest interger eg 5.4. 5.9 becomes 5)
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60); // takeing timer and minusing the number of seconds used up for the current minute and rounding up to the nearest interger
        timerText.text = string.Format("{0:00} {1:00}", minutes, seconds); // {0:00} = first 0 is mimutes, double 0 is number of digits it shows.  
    }*/

    public void DailyCrewCharge()
    {
        // removing resources for each crew member
        Crew[] currentCrew = FindObjectsOfType<Crew>();
    
        EventManager.Instance.RemoveResoruce("food", 1 * currentCrew.Length);
        EventManager.Instance.RemoveResoruce("water", 1 * currentCrew.Length);
        ResourceNegativeCheck();
    }

    public void NewWeightGoal()
    {
        // update new weight goal - weight manager -
        int newGoal = WeightManager.Instance.generateDayWeightLossTarget();
        WeightManager.Instance.targetWeightLoss = newGoal;
        dailyEject = newGoal;
    }

    public void GenerateRandomEvent()
    {
        // random 0 - 1 good / bad event -
        int rand = Random.Range(0, 2);
        if(rand == 0) dailyEvent = EventManager.Instance.GenerateGoodEvents();
        else dailyEvent = EventManager.Instance.GenerateBadEvents();
        UIManager.Instance.GetEventDescription(dailyEvent);
    }

    public void ResourceNegativeCheck()
    {
        for(int i =0; i < UIManager.Instance.resourceCounts.Length; i++)
        {
            if (UIManager.Instance.resourceCounts[i] < 0)
            {
                GameManager.Instance.GameOver();
                //insert a reference to a function within the UI Manager that handles the game over screen
            }
        }
    }
}
