using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ChartSerializable
{
    public string songName;
    public AudioClip music;
    public List<string> beat;
    public List<List<Vector2>> beatInt;

}
