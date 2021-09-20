using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ChartCreator : MonoBehaviour
{

    public AudioSource audio;
    Keyboard inputs;
    bool pause;
    private float startTime;
    public float bpm; 
    private float journeyLength;
      public Transform startMarker;
    public Transform endMarker;
    public Transform lime;
    public Text text;
    public float timeSet;
    bool onPlay;
    public bool topDown;
    void Start()
    {
         inputs = InputSystem.GetDevice<Keyboard>();
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
        Time.timeScale = 0;
    }
    public void ChangeTimeSet(float time)
    {
        timeSet = time;
    }
    public void Chagne(float bmp)
    {
        bpm = (int)bmp;
        text.text = bpm + "";

    }
    public void PauseOnTime()
    {
        Time.timeScale = 0;
        audio.Pause();
        onPlay = false;
    }
    void Update()
    {
        if (audio.time >= timeSet+2f)
        {
            PauseOnTime();
        }
        if (inputs.spaceKey.wasReleasedThisFrame)
        {
            pause = !pause;
            if (pause)
            {
            PauseOnTime();
            }
          else
            {
                Time.timeScale = 1;
                audio.time = timeSet; 
                audio.Play();
                onPlay = true;
            }
        }
        if (onPlay)
        {
        float distCovered = (Time.time - startTime) * bpm;
            Debug.Log(distCovered);
            lime.localPosition = new Vector3(lime.localPosition.x, Mathf.PingPong(distCovered, journeyLength), lime.localPosition.z);
        }
    }
}
