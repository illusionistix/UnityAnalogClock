using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI clipText;

    [SerializeField] private TMP_InputField hoursInput;
    [SerializeField] private TMP_InputField minutesInput;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider pitchSlider;

    private AudioSource mainAudioSource;

    private int hours;
    private int minutes;
    private int seconds;

    private int hoursInputValue;
    private int minutesInputValue;

    private bool alarmActivated;
    private bool isAudioTriggered;


    private void Start()
    {
        alarmActivated = false;
        isAudioTriggered = false;

        mainAudioSource = GetComponent<AudioSource>();
        mainAudioSource.Stop();

        hours = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;
        seconds = System.DateTime.Now.Second;

        hoursInputValue = 0;
        minutesInputValue = 0;

        volumeSlider.value = 1f;
        pitchSlider.value = 1f;

        clipText.text = "audio clip: " + mainAudioSource.clip.name;
    }


    public void SetAlarm()
    {
        if (hoursInput.text != "" && minutesInput.text != "")
        {
            int[] hoursInputArray = hoursInput.text.ToIntArray();
            int[] minutesInputArray = minutesInput.text.ToIntArray();

            if (hoursInputArray.Length > 1)
            {
                hoursInputValue = ((hoursInputArray[0] - 48) * 10) +
                    (hoursInputArray[1] - 48);
            }
            else
            {
                hoursInputValue = hoursInputArray[0] - 48;
            }

            if (minutesInputArray.Length > 1)
            {
                minutesInputValue = ((minutesInputArray[0] - 48) * 10) +
                    (minutesInputArray[1] - 48);
            }
            else
            {
                minutesInputValue = minutesInputArray[0] - 48;
            }


            //Debug.Log(hoursInputValue + " : " + minutesInputValue);
        }
    }


    public void PreviewAudioClip()
    {
        if (!mainAudioSource.isPlaying)
        {
            mainAudioSource.Play();
        }
        else
        {
            mainAudioSource.Stop();
        }
    }


    private void UpdateTimeText()
    {
        hours = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;
        seconds = System.DateTime.Now.Second;

        if (hours == hoursInputValue && minutes == minutesInputValue && seconds == 0)
        {
            alarmActivated = true;
        }

        string hoursText;
        string minutesText;
        string secondsText;

        if (hours < 10)
        {
            hoursText = "0" + hours.ToString();
        }
        else
        {
            hoursText = hours.ToString();
        }

        if (minutes < 10)
        {
            minutesText = "0" + minutes.ToString();
        }
        else
        {
            minutesText = minutes.ToString();
        }

        if (seconds < 10)
        {
            secondsText = "0" + seconds.ToString();
        }
        else
        {
            secondsText = seconds.ToString();
        }

        timeText.text = hoursText + ":" + minutesText + ":" + secondsText;
    }


    private void Update()
    {
        UpdateTimeText();

        mainAudioSource.volume = volumeSlider.value;
        mainAudioSource.pitch = pitchSlider.value;

        if (alarmActivated)
        {
            if (!isAudioTriggered)
            {
                isAudioTriggered = true;
                mainAudioSource.Play();
            }

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mainAudioSource.Stop();

            isAudioTriggered = false;
            alarmActivated = false;
        }
    }
}