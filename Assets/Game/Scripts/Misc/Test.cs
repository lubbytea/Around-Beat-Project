using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public string scene;
    public int aux;
    public Transform auxiliar;
    public Transform auxiliarDeMantenimeinto;// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(waitloksea());
        };
        aux = Mathf.Clamp(aux,0,100);

      //  auxiliar.position = new Vector3(aux, 0, 0);

        auxiliar.LookAt(auxiliarDeMantenimeinto);
    }
    
    IEnumerator waitloksea()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(scene);
    }
}
