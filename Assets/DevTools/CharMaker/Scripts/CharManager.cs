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
    public void Start()
    {
        if (runTest)
        {
        RunTest();
        }
        values = songChart.beat[0];
        CraftCharts();
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
        songChart.beat = new List<List<Vector2>>();
        songChart.beat.Add( new List<Vector2>());
        int x = -180;
        for (int y = 0; y < 8; y++)
        {
            x += 45;
        for (int i = -45; i < 45; i+=13)
        {
                // x += 2;
                songChart.beat[0].Add(new Vector2(i,x));
        }
        }
       
    }
}
