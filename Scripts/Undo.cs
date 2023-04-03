using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : MonoBehaviour
{
    private GameObject[] allObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
       
    }
    public void Awake()
    {
      allObjects = GameObject.FindGameObjectsWithTag("Destroyed");
      int len = allObjects.Length;
      Destroy(allObjects[len-1]);
    }
}
