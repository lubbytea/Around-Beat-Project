using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spheres : MonoBehaviour
{
    public GameObject hitFX;
    public GameObject pass,pass2;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMat(bool yes)
    {
        if (yes)
        {
            pass.SetActive(true);
        }
        else
        {
            pass.SetActive(false);
            pass2.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponent<Hand>() != null)
        {
            //    Destroy(this.gameObject);
            Instantiate(hitFX, this.transform);
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.name);
    //    if (other.GetComponent<Hand>() != null)
    //    {
    //        //    Destroy(this.gameObject);
    //              }
    //}

 

}
