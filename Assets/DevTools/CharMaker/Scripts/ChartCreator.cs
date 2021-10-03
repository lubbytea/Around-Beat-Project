using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ChartCreator : MonoBehaviour
{
    public AudioSource audioS;
    Keyboard inputs;
    Mouse mouse;
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
    public Transform currentChart;
    public EventSystem eventSystem;
    public Text songText;
    [Header("SONG HERE:")]
    public SongChart songChart;
    public GameObject point;
    public Transform canvasChart;
 
    void Start()
    {
        songText.text = songChart.songName;
        audioS.clip = songChart.music;
        inputs = InputSystem.GetDevice<Keyboard>();
        mouse = InputSystem.GetDevice<Mouse>();
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
        audioS.Pause();
        onPlay = false;
    }
    public void FillChart()
    {
       // songChart.music.length;
       // foreach (var item in collection)
        {
        songChart.beat.Add(new List<Vector2>());

        }
    }
  
    void Update()
    {
        if (mouse.leftButton.wasReleasedThisFrame && inputs.ctrlKey.IsPressed())
        {
            Instantiate(point, mouse.position.ReadValue(),Quaternion.Euler(0,0,0), canvasChart);
            currentChart.position = mouse.position.ReadValue();
        }
        if (eventSystem.currentSelectedGameObject!=null)
        {
        currentChart = eventSystem.currentSelectedGameObject.transform;
        if (mouse.leftButton.IsPressed()&&currentChart.tag=="point")
        {
        currentChart.position = mouse.position.ReadValue();
        }
        }
        if (audioS.time >= timeSet+2f)
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
                audioS.time = timeSet; 
                audioS.Play();
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
