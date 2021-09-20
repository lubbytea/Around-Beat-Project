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
    bool onPlay;
    void Start()
    {
         inputs = InputSystem.GetDevice<Keyboard>();
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }
    public void Chagne(float bmp)
    {
        bpm = (int)bmp;
        text.text = bpm + "";

    }
   
    void Update()
    {
        if (inputs.spaceKey.wasReleasedThisFrame)
        {
            pause = !pause;
            if (pause)
            {
                audio.Pause();
                onPlay = false;
            }
          else
            {
                audio.Play();
                onPlay = true;
            }
        }
        if (onPlay)
        {
        float distCovered = (Time.time - startTime) * bpm;
        lime.localPosition = new Vector3(lime.localPosition.x, Mathf.PingPong(distCovered, journeyLength), lime.localPosition.z);
        }
    }
}
