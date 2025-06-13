using System;
using UnityEngine;
using UnityEngine.UI;

public class DayAndNight : MonoBehaviour
{
    public Text txtTime;
    public float TimeSpeed = 3600;

    public void Update()
    {
        DateTime time = DateTime.Now;
        float second = (time.Hour * 3600) + (time.Minute * 60) + time.Second;
        second = (second * TimeSpeed) % 86400;
        int gameHours = Mathf.FloorToInt(second / 3600);
        int gameMinutes = Mathf.FloorToInt((second / 3600) / 60);
        string timeFormat = string.Format("{0:00}:{1:00}",gameHours, gameMinutes);
        txtTime.text = timeFormat;
    }
}
