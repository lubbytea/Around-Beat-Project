using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsWait : MonoBehaviour
{
    public GameObject hand1, hand2;
    public float time;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitforthehands());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator waitforthehands()
    {
        yield return new WaitForSeconds(time);
        hand1.SetActive(true);
        hand2.SetActive(true);
    }
}

