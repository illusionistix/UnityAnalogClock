
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalogClock : MonoBehaviour
{
    [SerializeField] private Text alarmText;
    [SerializeField] private LineRenderer pointerLinePrefab;
    
    private LineRenderer secondsPointer;
    private LineRenderer minutesPointer;
    private LineRenderer hoursPointer;

    private LineRenderer markerLine;

    // Start is called before the first frame update
    void Start()
    {
        alarmText.gameObject.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        int msec = System.DateTime.Now.Millisecond;
        int sec = System.DateTime.Now.Second;
        int min = System.DateTime.Now.Minute;
        int hour = System.DateTime.Now.Hour;
        float twoPi = 2 * Mathf.PI;

        float freqYseconds = Mathf.Sin(((msec / 60000f) + (sec / 60f)) * twoPi);
        float freqZseconds = -Mathf.Cos(((msec / 60000f) + (sec / 60f)) * twoPi);

        float freqYminutes = Mathf.Sin((((msec / 3600000f) + (sec / 3600f)) + (min / 60f)) * twoPi);
        float freqZminutes = -Mathf.Cos((((msec / 3600000f) + (sec / 3600f)) + (min / 60f)) * twoPi);

        float freqYhours = Mathf.Sin(((((msec / 43200000f) + (sec / 43200f)) + (min / 720f)) + (hour / 12f)) * twoPi);
        float freqZhours = -Mathf.Cos(((((msec / 43200000f) + (sec / 43200f)) + (min / 720f)) + (hour / 12f)) * twoPi);

        secondsPointer.SetPosition(1, new Vector3(0f, freqYseconds, freqZseconds) * 6f);
        minutesPointer.SetPosition(1, new Vector3(0f, freqYminutes, freqZminutes) * 6f);
        hoursPointer.SetPosition(1, new Vector3(0f, freqYhours, freqZhours) * 4f);

        //Debug.Log("");

        if (System.DateTime.Now.Second == 00 && System.DateTime.Now.Minute == 38)
        {
            alarmText.gameObject.SetActive(true);
        }
    }
}
