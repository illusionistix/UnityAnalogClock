using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    [SerializeField] private LineRenderer pointerLinePrefab;
    private LineRenderer secondsPointer;
    private LineRenderer minutesPointer;
    private LineRenderer hoursPointer;

    // Start is called before the first frame update
    void Start()
    {
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
        float freqYseconds = Mathf.Sin((System.DateTime.Now.Second / 60f) * (2 * Mathf.PI));
        float freqZseconds = -Mathf.Cos((System.DateTime.Now.Second / 60f) * (2 * Mathf.PI));

        float freqYminutes = Mathf.Sin((System.DateTime.Now.Minute / 60f) * (2 * Mathf.PI));
        float freqZminutes = -Mathf.Cos((System.DateTime.Now.Minute / 60f) * (2 * Mathf.PI));

        float freqYhours = Mathf.Sin((System.DateTime.Now.Hour / 12f) * (2 * Mathf.PI));
        float freqZhours = -Mathf.Cos((System.DateTime.Now.Hour / 12f) * (2 * Mathf.PI));

        secondsPointer.SetPosition(1, new Vector3(0f, freqYseconds, freqZseconds) * 6f);
        minutesPointer.SetPosition(1, new Vector3(0f, freqYminutes, freqZminutes) * 6f);
        hoursPointer.SetPosition(1, new Vector3(0f, freqYhours, freqZhours) * 4f);
    }
}
