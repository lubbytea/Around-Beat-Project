using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class ChartCreator : MonoBehaviour
{
    public AudioSource audioS;
    Keyboard inputs;
    Mouse mouse;
    public bool pause;
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

    public SongChart songChart;
    public GameObject point, pointB;
    public Transform canvasChart;
    float n = 0;
    public int maxValue;
    bool down = false;
    public GameObject upS, downS;
    public GameObject father, btn1, btn2;
    bool howFarWeGot;
    List<GameObject> listBtn;
    int id = 0;
    public List<List<Vector3>> beat;
    List<GameObject> notas;
    int currentIDForDelete=0;
    [Header("SONG HERE:")]
    public TextAsset textFile;
     ChartSerializable serializable;
    public string route;
    float lastBeatTime;
    bool paused=true;
    void Start()
    {
        beat = new List<List<Vector3>>();
        notas = new List<GameObject>();
        listBtn = new List<GameObject>();
        songText.text = songChart.songName;
        audioS.clip = songChart.music;
        inputs = InputSystem.GetDevice<Keyboard>();
        mouse = InputSystem.GetDevice<Mouse>();
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
        // Time.timeScale = 0;
    }

    public void ChangeTimeSet(float time)
    {
        timeSet = time;
    }
    public void Chagne(float bmp)
    {
        bpm = bmp;
        text.text = bpm + "";

    }
    public void FillChart()
    {
        // songChart.music.length;
        // foreach (var item in collection)
        {
            songChart.beat.Add(new List<Vector2>());

        }
    }
    public void DeleteChart()
    {
        beat[id].RemoveAt(currentChart.GetComponent<Hello>().id);
         Destroy(currentChart.gameObject);
    }
    void Update()
    {
        if (mouse.leftButton.wasReleasedThisFrame && inputs.ctrlKey.IsPressed())
        {
          GameObject g = (GameObject) Instantiate(point, mouse.position.ReadValue(), Quaternion.Euler(0, 0, 0), canvasChart);
            beat[id].Add(new Vector3(g.transform.localPosition.x, g.transform.localPosition.y,0));
            g.GetComponent<Hello>().id = currentIDForDelete;
            currentIDForDelete++;
            notas.Add(g);
        }
        if (mouse.leftButton.wasReleasedThisFrame && inputs.altKey.IsPressed())
        {
            GameObject g = (GameObject)Instantiate(pointB, mouse.position.ReadValue(), Quaternion.Euler(0, 0, 0), canvasChart);
            beat[id].Add(new Vector3(g.transform.localPosition.x, g.transform.localPosition.y, 1));
            g.GetComponent<Hello>().id = currentIDForDelete;
            currentIDForDelete++;
            notas.Add(g);
        }
        if (eventSystem.currentSelectedGameObject != null)
        {
              currentChart = eventSystem.currentSelectedGameObject.transform;
               
               if (mouse.leftButton.IsPressed()&&currentChart.tag=="point")
               {
               currentChart.position = mouse.position.ReadValue();
                beat[id][currentChart.GetComponent<Hello>().id] = currentChart.localPosition;
               }
        }
        /* if (audioS.time >= timeSet+2f)
         {
             PauseOnTime();
         }*/
        if (inputs.spaceKey.wasReleasedThisFrame)
        {
            pause = !pause;
            /*{
             //   Time.timeScale = 1;
                audioS.time = timeSet; 
                audioS.Play();
                onPlay = true;
            }*/
        }
        if (inputs.backspaceKey.wasReleasedThisFrame)
        {
            DeleteChart();
        }
        if (inputs.leftArrowKey.wasReleasedThisFrame|| inputs.aKey.wasReleasedThisFrame)
        {
            ClearMap();
            ChangeBeat(true);
            CallMap();
        }
        if (inputs.rightArrowKey.wasReleasedThisFrame || inputs.dKey.wasReleasedThisFrame)
        {
            ClearMap();
            ChangeBeat(false);
            CallMap();
        }
        lime.localPosition = new Vector3(lime.localPosition.x, n, lime.localPosition.z);

    }
    public void CreateSongBeatCharts()
    {
        float cuatity = audioS.clip.length / bpm;
        Debug.Log((int)cuatity);
        for (int i = 0; i < cuatity; i++)
        {
            howFarWeGot = !howFarWeGot;
            if (howFarWeGot)
            {
                GameObject tile = (GameObject)Instantiate(btn1, father.transform);
                TextMeshProUGUI v = tile.GetComponentInChildren<TextMeshProUGUI>();
                v.text = i + "";
                listBtn.Add(tile);
            }
            else
            {
                GameObject tile = (GameObject)Instantiate(btn2, father.transform);
                TextMeshProUGUI v = tile.GetComponentInChildren<TextMeshProUGUI>();
                v.text = i + "";
                listBtn.Add(tile);
            }

        }
        listBtn[0].GetComponentInChildren<Image>().transform.GetComponent<Image>().color = Color.black;
        for (int i = 0; i < cuatity; i++)
        {
            List<Vector3> currentBeat = new List<Vector3>();
            beat.Add(currentBeat);
        }

    }
    public void RunTest()
    {
        List<string> prueba = new List<string>();
        ChartSerializable myObject = new ChartSerializable();
        myObject.music = songChart.music;
        myObject.songName = songChart.songName;
        string toJson = "";
        foreach (var beater in beat)
        {
            foreach (var item in beater)
            {
                toJson += item;
            }
            prueba.Add(toJson);
            toJson = "";
        }
        myObject.beat = prueba;
        string json = JsonUtility.ToJson(myObject);
        System.IO.File.WriteAllText(route + "/" + songChart.songName + ".json", json);
    }
    void ChangeBeat(int cuantity)
    {
        listBtn[cuantity - 1].GetComponentInChildren<Hello>().transform.GetComponent<Image>().color = Color.clear;
        listBtn[cuantity].GetComponentInChildren<Hello>().transform.GetComponent<Image>().color = Color.black;
    }
    void ChangeBeat(bool der)
    {
        if (paused)
        {
            down = !down;
            if (down)
            {           
                upS.SetActive(false);
                downS.SetActive(true);
            }
            else
            {
                upS.SetActive(true);
                downS.SetActive(false);
            }
            if (!der)
        {
            id++;
            listBtn[id - 1].GetComponentInChildren<Hello>().transform.GetComponent<Image>().color = Color.clear;
            listBtn[id].GetComponentInChildren<Hello>().transform.GetComponent<Image>().color = Color.black;
                lastBeatTime += bpm;
                audioS.time = lastBeatTime;
            }
        else
        {
            id--;
            listBtn[id + 1].GetComponentInChildren<Hello>().transform.GetComponent<Image>().color = Color.clear;
            listBtn[id].GetComponentInChildren<Hello>().transform.GetComponent<Image>().color = Color.black;
                lastBeatTime -= bpm;
                if (lastBeatTime<0)
                {
                    lastBeatTime = 0;
                }
                audioS.time = lastBeatTime;
            }
        }
    }
    public void MakeLerp()
    {
        paused = false;
        audioS.Play();
        StartCoroutine(SimpleLerp(bpm));
    }
    IEnumerator SimpleLerp(float x)
    {
        int a, b;
        if (down)
        {
            a = 0;
            b = maxValue;
            upS.SetActive(false);
            downS.SetActive(true);
        }
        else
        {
            a = maxValue;
            b = 0;
            upS.SetActive(true);
            downS.SetActive(false);
        }
        for (float f = 0; f <= x; f += Time.deltaTime)
        {
            n = Mathf.Lerp(a, b, f / x);
            yield return null;

        }
        n = b;
        if (pause)
        {

            audioS.Pause();
            audioS.time = lastBeatTime;
            paused = true;
        }
        else
        {
            lastBeatTime = audioS.time;
            down = !down;
            id++;
            ChangeBeat(id);
            ClearMap();
            CallMap();
            StartCoroutine(SimpleLerp(x));
        }
    }
    void ClearMap()
    {
        foreach (var item in notas)
        {
            Destroy(item);
        }
        notas.Clear();
        currentIDForDelete = 0;


    }
   
    void CallMap()
    {
        foreach (var item in beat[id])
        {
            GameObject g;
            if (item.z==0)
            {
                g = (GameObject)Instantiate(point, canvasChart.TransformPoint(new Vector2(item.x, item.y)), Quaternion.Euler(0, 0, 0), canvasChart);
            }
            else
            {
                g = (GameObject)Instantiate(pointB, canvasChart.TransformPoint(new Vector2(item.x, item.y)), Quaternion.Euler(0, 0, 0), canvasChart);
            }
         
            g.GetComponent<Hello>().id = currentIDForDelete;
            currentIDForDelete++;
            notas.Add(g);
        }
        
    }
    public void ReadChartString()
    {
        List<List<Vector3>> beatTemp = new List<List<Vector3>>();
        serializable = JsonUtility.FromJson<ChartSerializable>(textFile.text);

        foreach (var beater in serializable.beat)
        {

            char[] b = beater.ToCharArray();
            float x = 0f;
            float y = 0f;
            float type = 0f;
            string current = "";
            bool punto = false;
            bool tercer = false;
            List<Vector3> tempList = new List<Vector3>();
            foreach (var item in b)
            {
                if (item == '.')
                {
                    punto = true;
                    continue;
                }
                if (punto == true)
                {
                    punto = false;
                    continue;
                }
                if (item == ')')
                {
                    type = float.Parse(current);
                    tempList.Add(new Vector3(x, y, type));
                    continue;
                }
                if (item == '(')
                {
                    x = 0;
                    y = 0;
                    current = "";
                    continue;
                }
                if (item == ',')
                {
                    if (tercer)
                    {
                        tercer = false;
                        y = float.Parse(current);
                        current = "";
                        continue;
                    }
                    x = float.Parse(current);
                    current = "";
                    tercer = true;
                    continue;
                }
                current += item;
            }
            beatTemp.Add(tempList);
        }
        beat = beatTemp;
    }

}
