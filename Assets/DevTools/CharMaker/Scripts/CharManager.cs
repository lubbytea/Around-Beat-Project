using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CharManager : MonoBehaviour
{
    public Transform spawner;
    public Transform center;
    public List<Vector3> values;
    public float offCenterValue;
    public float addedValueX;
    public GameObject chart,chartB;
    public bool runTest;
    public float addedValue;
    public int anchor;
    public SongChart songChart;
    public ChartSerializable serializable;
    public List<List<Vector3>> beat;
    public TextAsset text;
    public Transform lime;
    float n = 0;
    int i;
    public float bpm;
    public int maxValue;
    bool down = false;
    public int id = 0;
    public AudioSource cancion;
    public float eutanacia;
    public void Start()
    {
        if (runTest)
        {
        RunTest();
        }
        ReadChartString();

        StartCoroutine(waitForSong());
    }
    private void Update()
    {
        lime.localPosition = new Vector3(lime.localPosition.x, n, lime.localPosition.z);
    }
    IEnumerator waitForSong()
    {
        yield return new WaitForSeconds(2f);
        cancion.Play();
        yield return new WaitForSeconds(eutanacia);
        StartCoroutine(CastMultiple());
        MakeLerp();
    }
    IEnumerator CastMultiple()
    {
        values = beat[i];
        CraftCharts();
        i++;
        yield return new WaitForSeconds(bpm);
        StartCoroutine(CastMultiple());
    }
    public void ReadChartString()
    {
        beat = new List<List<Vector3>>();
         serializable = JsonUtility.FromJson<ChartSerializable>(text.text);
     
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
            if (item.z==0)
            {
                Instantiate(chart, spawner.position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(chartB, spawner.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }
     void RunTest()
    {
    List<string> prueba = new List<string>();
    beat = new List<List<Vector3>>();


        int x = -180;
        for (int y = 0; y < 8; y++)
        {
          List<Vector3>  currentBeat = new List<Vector3>();
            x += 45;
        for (int i = -45; i < 45; i+=13)
        {
                // x += 2;
                currentBeat.Add(new Vector3(i,x));
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
