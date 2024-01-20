
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnalogClock : MonoBehaviour
{
    //[SerializeField] private Text alarmText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private LineRenderer pointerLinePrefab;
    
    private LineRenderer secondsPointer;
    private LineRenderer minutesPointer;
    private LineRenderer hoursPointer;

    private LineRenderer markerLine;

    private int msec;
    private int hour;
    private int min;
    private int sec;

    private float twoPi;


    void Start()
    {

        twoPi = 2 * Mathf.PI;


        //alarmText.gameObject.SetActive(false);
        for (int i = 0; i < 60; i++)
        {
            float freqY = Mathf.Sin((i / 60f) * (2 * Mathf.PI));
            float freqZ = -Mathf.Cos((i / 60f) * (2 * Mathf.PI));

            markerLine = Instantiate(pointerLinePrefab, transform.position, pointerLinePrefab.transform.rotation);
            
            markerLine.SetPosition(0, new Vector3(0f, freqY, freqZ) * 4.5f);
            markerLine.SetPosition(1, new Vector3(0f, freqY, freqZ) * 4.9f);

            markerLine.endWidth = 0.1f;

            //markerLine.startColor = Color.clear;

            if (i % 5 == 0)
            {
                markerLine.startWidth = 0.2f;
                markerLine.endWidth = 0.2f;
                markerLine.SetPosition(0, new Vector3(0f, freqY, freqZ) * 4.25f);
            }

            if (i % 15 == 0)
            {
                markerLine.SetPosition(0, new Vector3(0f, freqY, freqZ) * 4f);
            }
        }        

        secondsPointer = Instantiate(pointerLinePrefab, transform.position, pointerLinePrefab.transform.rotation);
        minutesPointer = Instantiate(pointerLinePrefab, transform.position, pointerLinePrefab.transform.rotation);
        hoursPointer = Instantiate(pointerLinePrefab, transform.position, pointerLinePrefab.transform.rotation);
                
        secondsPointer.endColor = Color.clear;
        minutesPointer.endColor = Color.clear;
        hoursPointer.endColor = Color.clear;
        
        minutesPointer.startWidth = 0.2f;
        hoursPointer.startWidth = 0.35f;
        
    }

    
    void Update()
    {
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;
        msec = System.DateTime.Now.Millisecond;

        UpdateTimeText();
        

        //float freqYseconds = Mathf.Sin(((msec / 60000f) + (sec / 60f)) * twoPi);
        //float freqZseconds = -Mathf.Cos(((msec / 60000f) + (sec / 60f)) * twoPi);

        float freqYseconds = Mathf.Sin((sec / 60f) * twoPi);
        float freqZseconds = -Mathf.Cos((sec / 60f) * twoPi);

        //float freqYminutes = Mathf.Sin((((msec / 3600000f) + (sec / 3600f)) + (min / 60f)) * twoPi);
        //float freqZminutes = -Mathf.Cos((((msec / 3600000f) + (sec / 3600f)) + (min / 60f)) * twoPi);

        float freqYminutes = Mathf.Sin((min / 60f) * twoPi);
        float freqZminutes = -Mathf.Cos((min / 60f) * twoPi);

        float freqYhours = Mathf.Sin(((((msec / 43200000f) + (sec / 43200f)) + (min / 720f)) + (hour / 12f)) * twoPi);
        float freqZhours = -Mathf.Cos(((((msec / 43200000f) + (sec / 43200f)) + (min / 720f)) + (hour / 12f)) * twoPi);

        secondsPointer.SetPosition(1, new Vector3(0f, freqYseconds, freqZseconds) * 6f);
        minutesPointer.SetPosition(1, new Vector3(0f, freqYminutes, freqZminutes) * 6f);
        hoursPointer.SetPosition(1, new Vector3(0f, freqYhours, freqZhours) * 4f);
        
    }



    private void UpdateTimeText()
    {
        string hoursText;
        string minutesText;
        string secondsText;

        if (hour < 10)
        {
            hoursText = "0" + hour.ToString();
        }
        else
        {
            hoursText = hour.ToString();
        }

        if (min < 10)
        {
            minutesText = "0" + min.ToString();
        }
        else
        {
            minutesText = min.ToString();
        }

        if (sec < 10)
        {
            secondsText = "0" + sec.ToString();
        }
        else
        {
            secondsText = sec.ToString();
        }

        timeText.text = hoursText + ":" + minutesText + ":" + secondsText;
    }

}
