using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spheres : MonoBehaviour
{
    public GameObject hitFX;
    public Material x1, x2;
    public MeshRenderer me;
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
            me.material = x1;
        }
        else
        {
            me.material = x2;
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

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }

}
