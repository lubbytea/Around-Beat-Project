using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CharManager : MonoBehaviour
{
    public Transform spawner;
    public Transform center;
    public List<Vector2> values;
    public float offCenterValue;
    public float addedValueX;
    public GameObject chart;
    public bool runTest;
    public float addedValue;
    public int anchor;
    public SongChart songChart;
    public ChartSerializable serializable;
    public List<List<Vector2>> beat;
    public TextAsset text;
    public Transform lime;
    float n = 0;
    int i;
    public float bpm;
    public int maxValue;
    bool down = false;
    int id = 0;
    public void Start()
    {
        if (runTest)
        {
        RunTest();
        }
        ReadChartString();

        StartCoroutine(CastMultiple());
        MakeLerp();
    }
    private void Update()
    {
        lime.localPosition = new Vector3(lime.localPosition.x, n, lime.localPosition.z);

    }
    IEnumerator CastMultiple()
    {
        yield return new WaitForSeconds(bpm);
        values = beat[i];
        CraftCharts();
        i++;
        StartCoroutine(CastMultiple());
    }
    public void ReadChartString()
    {
        beat = new List<List<Vector2>>();
         serializable = JsonUtility.FromJson<ChartSerializable>(text.text);
     
        foreach (var beater in serializable.beat)
        {

        char[] b = beater.ToCharArray();
        float x=0f;
        float y=0f;
        string current="";
        bool punto = false;
         List<Vector2> tempList = new List<Vector2>();
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
                y = float.Parse(current);
                tempList.Add(new Vector2(x,y));
                continue;
            }
            if (item =='(')
            {
                x= 0;
                y= 0;
                current = "";
                continue;
            }
            if (item == ',')
            {
                x = float.Parse(current);
                current = "";
                continue;
            }
            current += item;
        }
            beat.Add(tempList);
        }

    }
    public void CraftCharts()
    {
        GameObject[] notas = GameObject.FindGameObjectsWithTag("Chart");
        foreach (var item in notas)
        {
           Destroy(item);
        }
        foreach (var item in values)
        {
            if (Mathf.Abs(item.x) > anchor)
            {
                spawner.localPosition = new Vector3(offCenterValue-((Mathf.Abs(item.y))*(Mathf.Abs(item.y)/ addedValueX) / addedValue), 0, 0);
            }
            center.rotation = Quaternion.Euler(0, item.x, 0);
            center.localPosition = new Vector3(0, item.y, 0);
            Instantiate(chart, spawner.position, Quaternion.Euler(0, 0, 0));
        }
    }
     void RunTest()
    {
    List<string> prueba = new List<string>();
    beat = new List<List<Vector2>>();


        int x = -180;
        for (int y = 0; y < 8; y++)
        {
          List<Vector2>  currentBeat = new List<Vector2>();
            x += 45;
        for (int i = -45; i < 45; i+=13)
        {
                // x += 2;
                currentBeat.Add(new Vector2(i,x));
        }
            beat.Add(currentBeat);
        }



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
        System.IO.File.WriteAllText(Application.persistentDataPath + "/"+ songChart.songName+ ".json", json);
    }


    // LERP
    public void MakeLerp()
    {
        StartCoroutine(SimpleLerp(bpm));
    }
    IEnumerator SimpleLerp(float x)
    {
        int a, b;
        if (down)
        {
            a = 0;
            b = maxValue;
      
        }
        else
        {
            a = maxValue;
            b = 0;
        }
        for (float f = 0; f <= x; f += Time.deltaTime)
        {
            n = Mathf.Lerp(a, b, f / x);
            yield return null;

        }
        n = b;
      
            down = !down;
            id++;
            StartCoroutine(SimpleLerp(x));
     }
    
}
