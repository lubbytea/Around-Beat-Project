using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="BEAT")
        {
            other.GetComponent<Spheres>().ChangeMat(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BEAT")
        {
            other.GetComponent<Spheres>().ChangeMat(false);
        }
    }
}
