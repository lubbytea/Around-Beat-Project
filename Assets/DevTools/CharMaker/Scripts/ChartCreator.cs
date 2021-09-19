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
    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * bpm;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.

        lime.localPosition = new Vector3(lime.localPosition.x, Mathf.PingPong(distCovered, journeyLength), lime.localPosition.z);
        Debug.Log(audio.time);
        if (inputs.spaceKey.wasReleasedThisFrame)
        {
            pause = !pause;
            if (pause)
                audio.Pause();
           
          else
                audio.Play();
        }
    }
}
