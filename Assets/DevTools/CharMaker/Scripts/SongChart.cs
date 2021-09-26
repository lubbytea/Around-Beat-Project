using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SongChart", order = 1)]
public class SongChart : ScriptableObject
{
    public string songName;
    public AudioClip music;
    public List<List<Vector2>> beat;

}
