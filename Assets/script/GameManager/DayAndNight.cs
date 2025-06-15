using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayAndNight : MonoBehaviour
{
    public Text txtTime;
    public float TimeSpeed = 60f;
    public Light2D light2D;
    public Gradient gradient;

    public void Update()
    {
        DateTime time = DateTime.Now;

        float second = (time.Hour * 3600) + (time.Minute * 60) + time.Second;
        second = (second * TimeSpeed) % 86400f;

        int gameHours = Mathf.FloorToInt(second / 3600f);
        int gameMinutes = Mathf.FloorToInt((second % 3600f) / 60f);
        string timeFormat = string.Format("{0:00}:{1:00}", gameHours, gameMinutes);
        txtTime.text = timeFormat;

        ChangeColorByTime(second);
    }

    public void ChangeColorByTime(float time)
    {
        light2D.color = gradient.Evaluate(time / 86400);
    }    
}
