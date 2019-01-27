using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trese : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Finish");

        foreach(GameObject o in objects)
        {
            if (o.name.Contains("Tree-1"))
            {
                GameObject g = Instantiate(prefab1);
                g.transform.position = o.transform.position;
                g.transform.rotation = o.transform.rotation;
                Debug.Log("1");
            }
            if (o.name.Contains("Tree-2"))
            {
                GameObject g = Instantiate(prefab2);
                g.transform.position = o.transform.position;
                g.transform.rotation = o.transform.rotation;
                Debug.Log("2");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
