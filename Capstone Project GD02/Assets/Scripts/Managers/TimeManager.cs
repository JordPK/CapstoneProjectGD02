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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DisplayTimer();

        currentTime += Time.deltaTime;

        // if currentTime hits dayDuration add increase the day by 1
        if (currentTime >= dayDuration)
        {
            currentTime = 0;
            GameManager.Instance.currentDay++;
            WeightManager.Instance.targetWeightLoss = WeightManager.Instance.generateDayWeightLossTarget();
            //Debug.Log(GameManager.Instance.currentDay);
        }
    }

    void DisplayTimer()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60.0f); // Dividing by 60 (number of seconds then going down to the lowest interger eg 5.4. 5.9 becomes 5)
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60); // takeing timer and minusing the number of seconds used up for the current minute and rounding up to the nearest interger
        timerText.text = string.Format("{0:00} {1:00}", minutes, seconds); // {0:00} = first 0 is mimutes, double 0 is number of digits it shows.  
    }
}
