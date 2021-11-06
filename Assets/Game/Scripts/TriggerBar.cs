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
            other.gameObject.GetComponent<Spheres>().ChangeMat(true);
            Debug.Log("asda");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BEAT")
        {
            other.gameObject.GetComponent<Spheres>().ChangeMat(false);
        }
    }
}
