using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject obk;
    bool fa;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Pause22()
    {
        fa=!fa;
        if (fa)
            obk.SetActive(true);
        else
            obk.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
