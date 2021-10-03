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
    public List<Vector2> tempList;
    public void Start()
    {
        if (runTest)
        {
        RunTest();
        }
        ReadChartString();
        values = tempList;
        CraftCharts();
    }
    public void ReadChartString()
    {
        serializable = JsonUtility.FromJson<ChartSerializable>(text.text);
        Debug.Log(serializable.beat);
        char[] b = serializable.beat[0].ToCharArray();
        float x=0f;
        float y=0f;
        string current="";
        bool punto = false;
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
                spawner.localPosition = new Vector3(offCenterValue-((Mathf.Abs(item.x))*(Mathf.Abs(item.x)/ addedValueX) / addedValue), 0, 0);
            }
            center.rotation = Quaternion.Euler(0, item.y, 0);
            center.localPosition = new Vector3(0, item.x, 0);
            Instantiate(chart, spawner.position, Quaternion.Euler(0, 0, 0));
        }
    }
     void RunTest()
    {
    List<string> prueba = new List<string>();
    beat = new List<List<Vector2>>();
        beat.Add( new List<Vector2>());
        int x = -180;
        for (int y = 0; y < 8; y++)
        {
            x += 45;
        for (int i = -45; i < 45; i+=13)
        {
                // x += 2;
                beat[0].Add(new Vector2(i,x));
        }
        }

        ChartSerializable myObject = new ChartSerializable();
        myObject.music = songChart.music;
        myObject.songName = songChart.songName;
        string pepita = "";
        foreach (var item in beat[0])
        {
            pepita += item;
        }
            string pepe = pepita;
            prueba.Add(pepe);
        prueba.Add(pepe);

        myObject.beat = prueba;
       
        string json = JsonUtility.ToJson(myObject);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/"+ songChart.songName+ ".json", json);
    }
}
